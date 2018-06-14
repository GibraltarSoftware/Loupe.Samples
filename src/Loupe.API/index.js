(function ($) {
    var accessToken,
        loggedIn = false,
        baseUrl = 'https://loupe-test.onloupe.com/',
        currentTenant,
        currentApplication,
        currentVersion,
        emptyGuid = '00000000-0000-0000-0000-000000000000',
        userTenants,
        removeContainer;

    
    $(function () {
        $('#baseUrl').val(baseUrl);
        $('#form').keypress(function () {
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
        $('#saveVersion').click(saveVersion);
        $('#newApplicationVersion').click(newApplicationVersion);
        $('ul.tabs').click(function (e) {
            removeContainer.hide();

            var selectedId = e.target.id,
                targetContainerId = e.target.id + "Container";


            $('ul.tabs').find('li').each(function () {
                if (this.id === selectedId) {
                    $(this).addClass("selected");
                } else {
                    $(this).removeClass("selected");
                }
            });

            $('.tab-content').each(function () {
                if (this.id === targetContainerId) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
        });
        $('#getApplicationVersions').click(bindVersions);
        $('#searchUserSessions').blur(searchUserSessions);
        $('#searchUserApps').blur(searchUserApps);
        $('#remove').click(removeAccount);
        removeContainer = $('#removeContainer');
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
        var username = $('#username').val(),
            password = $('#password').val();
        var authToken = btoa(username + ':' + password);
        var settings = {
            url: baseUrl + 'api/auth/token',
            headers: {
                'Authorization': 'Basic ' + authToken
            }
        };

        $('#invalid').hide();

        // authenticate the user to get the token
        $.ajax(settings)
            .done(function (response) {
                accessToken = response.access_token;
                $('#loggedIn').show();
                loggedIn = true;
                $('#login').val("Logout");

                getTenants();
            })
            .fail(function () {
                $('#loggedIn').hide();
                $('#invalid').show();
            });
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
        removeContainer.hide();

        currentTenant = tenant;
        selectListItem('tenants', tenant);
        setBaseUrl(tenant);

        var settings = callSettings('api/Tenant/Select/' + tenant, false, 'POST');

        $.ajax(settings).done(function () {
            getApplications();
        });
    }

    // many calls in Loupe require the product and application, so fetch all these to allow user selection
    function getApplications() {
        var settings = callSettings('Customers/' + currentTenant + '/api/Application/AllProductsAndApplications');

        $.ajax(settings).done(function (response) {
            var applications = $('#applications'),
                dataKey = 'loupe-application';
            applications.empty();
            $.each(response,
                function (idx, app) {
                    $('<li/>')
                        .text(app.productApplicationCaption)
                        .data(dataKey, app)
                        .click(function () {
                            var selectedApp = $(this).data(dataKey);
                            setApplication(selectedApp);
                        })
                        .appendTo(applications);
                });

            $('#applicationsContainer').show();
        });
    }

    function bindVersions() {
        var url = '/api/ApplicationVersion/Versions?releaseTypeId=' + emptyGuid +
            '&take=0&skip=0' + // note - these two parameters are legacy options and are unused
            '&page=1&pageSize=10';
        var settings = callSettings('Customers/' + currentTenant + url, true);

        $.ajax(settings).done(function (response) {
            $('#versionsContainer').show();

            var applicationVersions = $('#applicationVersions'),
                dataKey = 'application-version';

            applicationVersions.empty();
            $.each(response.data,
                function (idx, version) {
                    $('<li/>')
                        .text(version.caption)
                        .data(dataKey, version)
                        .click(function () {
                            var selectedVersion = $(this).data(dataKey);
                            getVersion(selectedVersion);
                        })
                        .appendTo(applicationVersions);
                });
        });
    }
    function setApplication(selectedApp) {
        removeContainer.hide();

        currentApplication = selectedApp;
        selectListItem('applications', selectedApp.productApplicationCaption);
        bindUsers();
        $('#tabsContainer').show();
    }

    function bindUsers() {
        var settings = callSettings('Customers/' + currentTenant + '/api/User/SystemUsers', true);

        var sessionsUsers = $('#sessionsUsers'),
            appsUsers = $('#appsUsers');

        $.ajax(settings).done(function (response) {
            $.each(response,
                function (idx, user) {
                    $('<option/>')
                        .val(user.id)
                        .text(user.caption)
                        .appendTo(sessionsUsers);
                    $('<option/>')
                        .val(user.id)
                        .text(user.caption)
                        .appendTo(appsUsers);
                });
        });
    }

    function createList(target, data, currentValue) {
        var targetElement = $('#' + target);

        $.each(data,
            function (idx, item) {
                var li = $('<option/>')
                    .val(item.id)
                    .text(item.caption)
                    .appendTo(targetElement);

                if (item.id === currentValue) {
                    li.addClass("selected");
                }
            });
    }

    function getVersion(selectedVersion) {
        $('#savedMessage').text();
        selectListItem('applicationVersions', selectedVersion.caption);

        var settings = callSettings('Customers/' + currentTenant + '/api/ApplicationVersion/Get/' + selectedVersion.id, true);
        $.ajax(settings).done(function (response) {
            var version = response.version;
            currentVersion = version;

            createList('releaseType', response.lists.releaseTypes, version.releaseType);
            createList('promotionLevel', response.lists.promotionLevels, version.promotionLevel);

            $("#applicationVersion").show();
            $('#displayVersion').val(version.displayVersion);
            $('#caption').val(version.caption);
            $('#description').val(version.description);
            $('#releaseDate').val(version.releaseDate);
            $('#releaseType').val(version.releaseType);
            $('#releaseNotesUrl').val(version.releaseNotesUrl);
            $('#promotionLevel').val(version.promotionLevel);
        });
    }

    function saveVersion() {
        if (currentVersion.id === emptyGuid) {
            currentVersion.version = $('#caption').val();
            saveApplicationVersion('Post');
        } else {
            saveApplicationVersion('Put');
        }
    }
    function saveApplicationVersion(action) {
        var settings = callSettings('Customers/' + currentTenant + '/api/ApplicationVersion/' + action, true, action.toUpperCase());

        var version = {
            id: currentVersion.id,
            applicationId: currentApplication.id,
            caption: $('#caption').val(),
            description: $('#description').val(),
            displayVersion: $('#displayVersion').val(),
            releaseDate: $('#releaseDate').val(),
            releaseNotesUrl: $('#releaseNotesUrl').val(),
            releaseType: $('#releaseType').val(),
            version: currentVersion.version,
            versionBin: currentVersion.versionBin
        };

        settings.data = version;

        $.ajax(settings)
            .done(function () {
                $('#savedMessage').text('saved');

                bindVersions();
                $('#saveErrors').empty();
            })
            .fail(function (response) {
                if (response.status === 400) {
                    $('#savedMessage').text('');
                    if (response.responseJSON && response.responseJSON.modelState) {
                        bindModelStateErrors('saveErrors', response.responseJSON.modelState);
                    } else {
                        $('#savedMessage').text(response.text);
                    }
                }
            });
    }

    function newApplicationVersion() {
        selectListItem('applicationVersions', '');

        // get a blank one first, which also gets the reference data
        var settings = callSettings('Customers/' + currentTenant + '/api/ApplicationVersion/GetNew', true);

        $.ajax(settings).done(function (response) {
            var version = response.version;
            currentVersion = version;

            createList('releaseType', response.lists.releaseTypes, version.releaseType);
            createList('promotionLevel', response.lists.promotionLevels, version.promotionLevel);

            $("#applicationVersion").show();
        });
    }

    function bindModelStateErrors(target, modelState) {
        var targetElement = $('#' + target);

        for (var prop in modelState) {
            if (modelState.hasOwnProperty(prop)) {
                $.each(modelState[prop],
                    function (idx, error) {
                        $('<li/>')
                            .text(error)
                            .appendTo(targetElement);
                    });
            }
        }
    }


    function createCell(row, content) {
        $('<td/>')
            .text(content)
            .appendTo(row);
    }

    function searchUserSessions() {
        var user = $('#searchUserSessions').val();

        if (!user) {
            return;
        }

        var settings = callSettings('Customers/' + currentTenant + '/api/ApplicationUsers/TopSessions/' + user, true);

        $.ajax(settings).done(function (result) {
            var target = $('#userSessions');
            target.empty();

            $.each(result,
                function (idx, session) {
                    var row = $('<tr/>');

                    createCell(row, session.applicationCaption);
                    createCell(row, session.startDateTime);
                    createCell(row, session.endDateTime);
                    createCell(row, session.criticalCount.count);
                    createCell(row, session.errorCount.count);
                    createCell(row, session.warningCount.count);
                    createCell(row, session.messageCount.count);

                    target.append(row);
                });
        });
    }

    function searchUserApps() {
        var user = $('#searchUserApps').val();

        if (!user) {
            return;
        }

        var settings = callSettings('Customers/' + currentTenant + '/api/ApplicationUsers/TopApplications/' + user, true);

        $.ajax(settings).done(function (result) {
            var target = $('#userApps');

            $.each(result,
                function (idx, application) {
                    var row = $('<tr/>');

                    createCell(row, application.applicationCaption);
                    createCell(row, application.version);
                    createCell(row, application.lastAccessed);

                    target.append(row);
                });
        });
    }

    function removeAccount() {
        var removeButton = $('#removeAccount');
        var actualRemove = $('#actualRemove');
        var removeAccountYes = $('#removeAccountYes');
        var removeAccountNo = $('#removeAccountNo');
        
        removeContainer.show();

        removeAccountYes.click(function () {

            var settings = callSettings('api/Account/Remove', false, 'POST');

            $.ajax(settings)
                .done(function() {
                    removeButton.show();
                    actualRemove.hide();
                    removeAccountYes.unbind("click");
                    removeContainer.hide();
                    logout();
                })
                .fail(function(response) {
                    if (response.status === 404 || response.status === 501) {
                        $('#NotImplemented').show();
                    }
                });
        });

        removeAccountNo.click(function () {
            removeButton.show();
            actualRemove.hide();
            removeAccountNo.unbind("click");
        });

        removeButton.hide();
        actualRemove.show();
    }

})(jQuery)