(function ($) {
    var accessToken,
        loggedIn = false,
        baseUrl = 'https://loupe-test.onloupe.com/',
        currentTenant,
        userTenants,
        selectedSession;

    $(function () {
        baseUrl = 'http://localhost:58080/';

        $('#baseUrl').val(baseUrl);
        $('#form').keypress(function (event) {
            if (loggedIn) {
                return true;
            }

            var code = event.keyCode ? event.keyCode : event.which;
            if (code === 13) {
                login();
                return false;
            }
            return true;
        });
        $('#login').click(function () {
            login();
        });
        $('#logSearch').blur(searchLogs);
        $('#searchSubmit').click(searchLogs);
    });

    function login() {
        if (loggedIn) {
            logout();
            loggedIn = false;
        }

        baseUrl = $('#baseUrl').val();
        if (baseUrl.endsWith('/') === false) {
            baseUrl += '/';
        }

        // Base64 encode the username and password, used to get the authentication token
        const username = $('#username').val();
        const password = $('#password').val();
        const authToken = btoa(username + ':' + password);
        const settings = {
            method: "GET",
            url: baseUrl + 'api/auth/token',
            headers: {
                'Authorization': 'Basic ' + authToken
            }
        };

        $('#invalid').hide();

        try {
            // authenticate the user to get the token
            $.ajax(settings)
                .done(function (response) {
                    accessToken = response.access_token;
                    $('#loggedIn').show();
                    loggedIn = true;
                    $('#login').val("Logout");

                    getTenants();
                })
                .fail(function (response) {
                    console.error("Login failed", response);
                    $('#loggedIn').hide();
                    $('#invalid').show();
                });
        }
        catch(error) {
            console.error(error);
            $('#loggedIn').hide();
            $('#invalid').show();
        }
    }

    function logout() {
        $('#loggedIn').hide();
        loggedIn = false;
        accessToken = null;
        $('#login').val("Login");
    }

    function selectListItem(target, value) {
        $('#' + target)
            .find('li')
            .each(function () {
                var item = $(this);

                if (item.text() === value) {
                    item.addClass('selected');
                } else {
                    item.removeClass('selected');
                }
            });
    }

    // create the basic settings for authenticated calls to Loupe
    function callSettings(url, isForApplication, method) {
        var settings = {
            url: baseUrl + url,
            headers: {
                'Authorization': 'Session ' + accessToken
            }
        };

        if (isForApplication) {
            // calls for a specific application need the product name and application name set as http headers
            settings.headers['loupe-product'] = currentApplication.productName;
            settings.headers['loupe-application'] = currentApplication.applicationName;
        }

        if (method) {
            settings.method = method;
        }

        return settings;
    }


    // most customers have a single tenant
    function getTenants() {
        var settings = callSettings('api/Tenant/ForUser');

        $('#tenants').empty();
        $.ajax(settings)
            .done(function (response) {
                userTenants = response.tenants;

                var tenants = $('#tenants');

                tenants.show();
                $.each(response.tenants,
                    function (idx, tenant) {
                        $('<li/>')
                            .text(tenant.tenantName)
                            .click(function (e) {
                                currentTenant = e.target.textContent;
                                setTenant(currentTenant);
                            })
                            .appendTo(tenants);
                    });

                if (response.tenants.length === 1) {
                    // if there's only one tenant, select it automatically
                    var tenant = response.tenants[0];
                    currentTenant = tenant.tenantName;

                    if (tenant.basePath) {
                        baseUrl = tenant.basePath;
                    }

                    setTenant(currentTenant);
                }
            });
    }

    // set the base url to be the base path of the tenant
    function setBaseUrl(tenant) {
        $.each(userTenants,
            function (t) {
                if (t.tenantName === tenant && t.basePath) {
                    baseUrl = t.basePath;
                }
            });
    }

    // selecting the tenant returns the user details for that tenant (user details may be different per tenant)
    function setTenant(tenant) {
        currentTenant = tenant;
        selectListItem('tenants', tenant);
        setBaseUrl(tenant);

        var settings = callSettings('api/Tenant/Select/' + tenant, false, 'POST');

        $.ajax(settings).done(function () {
            getSessions();
        });
    }

    // get the sessions within the last full day (ie yesterday)
    function getSessions() {
        const yesterdayStart = new Date(new Date().setDate(new Date().getDate()-1));
        const yesterdayEnd = new Date(new Date().setDate(new Date().getDate()-1));
        yesterdayStart.setUTCHours(0,0,0,0);
        yesterdayEnd.setUTCHours(23, 59, 59, 999);

        const settings = callSettings(`Customers/${currentTenant}/api/Sessions/SessionsFiltered`, false, 'POST');

        const data =  {
            query: {
                filters: [
                    {column: "StartDateTime", operator: 6, value1: yesterdayStart.toISOString(), value2: yesterdayEnd.toISOString()},
                    {column: "EndDateTime", operator: 6, value1: yesterdayStart.toISOString(), value2: yesterdayEnd.toISOString()}
                ]
            },
            skip: 0,
            take: 10
        };
        settings.contentType = "application/json";
        settings.dataType = "json";
        settings.data = JSON.stringify(data);

        $.ajax(settings).done(function (result) {
            $('#sessionsContainer').show();
            var target = $('#sessions');
            target.empty();

            $.each(result.data,
                function (idx, session) {
                    var row = $('<tr/>')
                        .click(function(e) {
                            selectedSession = session;
                            getLog(session.id);
                        });

                    createCell(row, session.application.badge.title);
                    createCell(row, session.startDateTime);
                    createCell(row, session.endDateTime);
                    createCell(row, session.userName);

                    target.append(row);
                }
            );
        });
    }

    // get the log messages for a given session
    function getLog(sessionId) {
        const settings = callSettings(`Customers/${currentTenant}/api/Log/GetMessages`, false, 'POST');
        settings.contentType = "application/json";
        settings.dataType = "json";
        const data = {
            id: sessionId,
            query: {
                filters: null,
                paging: {
                    skip: 0,
                    take: 100
                },
                sort: [
                    { column: "Timestamp", direction: 0}
                ]
            },
            includeDetails: true
        };
        settings.data = JSON.stringify(data);

       getLogMessages(settings);
    }

    // search for log messages
    function searchLogs() {
        var searchString = $('#logSearch').val();

        if (!searchString) {
            return;
        }

        const settings = callSettings(`Customers/${currentTenant}/api/Log/GetMessages`, false, 'POST');
        settings.contentType = "application/json";
        settings.dataType = "json";
        const data = {
            id: selectedSession.id,
            query: {
                filters: [
                    { column: "Search", operator: 7, value1: searchString, value2: null}
                ]
            },
            includeDetails: true
        };
        settings.data = JSON.stringify(data);

        getLogMessages(settings);
    }


    function getLogMessages(settings) {
        $.ajax(settings).done(function (result) {
            $('#logsContainer').show();
            var target = $('#log');
            target.empty();

            $.each(result.data,
                function (idx, logMessage) {
                    var row = $('<tr/>');

                    createCell(row, logMessage.timestamp);
                    createCell(row, logMessage.user && logMessage.user.caption);
                    createCell(row, logMessage.caption);

                    target.append(row);
                }
            );
        });
    }


    function createCell(row, content) {
        $('<td/>')
            .text(content)
            .appendTo(row);
    }

})(jQuery)