# Loupe API Sample Project
This project is a simple Javascript project using jQuery to call into the Loupe API. It shows the 
authentication process as well as showing a few basic calls, explaining the URL format and responses.
## Request Format
All requests are JSON based.
# Authentication
Authenticated requests require a session token, passed as the `Authorization` header. To obtain
this token pass the user name and password as a Base64 encoded string using in the `Authorization` 
header. 

For example:
```javascript
var baseUrl = 'https://loupe.gibraltarsoftware.com/';
var username = 'your username',
	password = 'your password',
	accessToken;
var authToken = btoa(username + ':' + password);
var settings = {
	url: baseUrl + 'api/auth/token',
	headers: {
		'Authorization': 'Basic ' + authToken
	}
};
$.ajax(settings)
	.done(function (response) {
		accessToken = response.access_token;
	});
```
Once the session token has been obtained, it should be passed in all subsequent calls that require 
authenticated access. 

For example:
```javascript
var settings = {
	url: baseUrl + 'api/controller/action',
	headers: {
		'Authorization': 'Session ' + accessToken
	}
};
```

# Tenants
The Loupe SAAS platform is multi-tenant, so requires selection of your tenant and this tenant name should be used as part of the url in all subsequent calls.

If you are self-hosting Loupe, then you may still be be multi-tenant, although most likely your installation will be single tenant, in which case you can omit the tenant details.

For multi-tenant, the  URL for most calls will be:

`/Customers/{tenant}/api/{controller}/{action}`

For single-tenant calls, simply omit the tenant portion:

`/api/{controller}/{action}`

## Available Tenants
The list of tenants for your user is available using the following call:

```javascript
var settings = {
	url: baseUrl + 'api/Tenant/ForUser',
	headers: {
		'Authorization': 'Session ' + accessToken
	}
};
$.ajax(settings)
	.done(function (response) {
		var tenants = response.tenants;
	});
```
This call will obtain a list of tenants that the user has an account in, and that are not expired
and are not redirected (ie, on a different cluster). A site administrator will see all tenants, 
except for the expired and redirected ones.

The call returns a list of `UserTenant` objects, each of which contains three properties:

* **TenantName**. The name of the tenant.
* **IsTenantUser**. Inndicates whether or not the user is an actual user in the tenant (a site
administator for example, may not ahve an actual account in all tenants).
* **BasePath**. The base path of the cluster for the repository.

> The **BasePath** should be used as the base url for all subsequent requests, since this is where the
> repository actually resides.

## Selecting a Tenant
Tenant selection isn't required, but may be useful if you need the user profile for that tenant. 

For example:

```javascript
var settings = {
	url: baseUrl + 'api/Tenant/Select/' + tenantName,
	method: 'POST',
	headers: {
		'Authorization': 'Session ' + accessToken
	}
};
$.ajax(settings)
	.done(function (response) {
		var userDetails = response;
	});
```

The user details returned are a `UserContext` object, containing:

* **id**. The ID of the user.
* **userName**. The user's login name.
* **displayName**. The user name displayed.
* **email**. The user's email address.
* **roles**. A list of roles for the user.
* **timeZoneCode**. The code for their time zone.
* **timeZoneOffset**. The offset from UTC for their tome zone.
* **ExternalAuth**. Indicates whether or not the user is authenticated externally (eg, via Active Directory).

# Products and Applications
Many Loupe calls require the product and application to filter data. To avoid URL manipulation, the
product and application names are not passed down to API calls in the URL. Instead they are passed as
custom headers in the request.

You can obtain a list of products and applications using the following code:

```javascript
var settings = {
	url: baseUrl + 'Customers/' + tenantName + 'api/Application/AllProductsAndApplications',
	headers: {
		'Authorization': 'Session ' + accessToken
	}
$.ajax(settings)
	.done(function (response) {
		var applications = response;
	});
```
The details returned is a list of `ProductApplicationCaptionModel` objects containing:

* **id**. The application id.
* **productName**. The product name.
* **applicationName**. The application name.
* **productCaption**. The product caption.
* **applicationCaption**. The application caption.
* **productApplicationCaption**. The combined product and application caption.

The latter of these is useful for display, since Loupe eliminates duplicate text 
when the application includes the product name.

The **productName** and **applicationName** should be used in all requests that
relate to a specific application.

# Use Cases for the Loupe API
The following section details a few simple use cases for the Loupe API, including example code.

## User Support Tool Integration
Imaging a CRM system with a side panel integrating into Loupe showing a summary
of users activity and problems. Here you'd want to look up these details by
supplying a user name or email address. For example:

```javascript
var user = 'john';
var url = 'Customers/' + currentTenant + '/api/ApplicationUsers/TopSessions/' + user;
var settings = {
	url: baseUrl + 'Customers/' + tenantName + url,
	headers: {
		'Authorization': 'Session ' + accessToken,
		'loupe-product': productName,
		'loupe-application': applicationName
	}
};
$.ajax(settings)
	.done(function (response) {
		var userSessions = response;
	});
```

## CRM Integration
CRM systems often detail the applications used within a company, but may not be able to indicate
which applications are in use for a given user. The following example details the application,
the version and when that was last used:

```javascript
var user = 'john';
var url = 'Customers/' + currentTenant + '/api/ApplicationUsers/TopApplications/' + user;
var settings = {
	url: baseUrl + 'Customers/' + tenantName + url,
	headers: {
		'Authorization': 'Session ' + accessToken,
		'loupe-product': productName,
		'loupe-application': applicationName
	}
};
$.ajax(settings)
	.done(function (response) {
		var userApplications = response;
	});
```


## Application Versions
Each application seen by Loupe is processed and its version details stored; 
this gives you tracking of deployment and issues across releases. Rather than waiting 
for Loupe to see and identify a new version, you can create one ahead of time, perhaps 
as part of your deployment process, enabling you to set details such as the version number, 
the release date and release type.

A list of application versions can be fetched using the following:

```javascript
var url = '/api/ApplicationVersion/Versions?releaseTypeId=' + releaseTypeId +
    '&take=0&skip=0' + 
    '&page=1&pageSize=10';
var settings = {
	url: baseUrl + 'Customers/' + tenantName + url,
	headers: {
		'Authorization': 'Session ' + accessToken,
		'loupe-product': productName,
		'loupe-application': applicationName
	}
};
$.ajax(settings)
	.done(function (response) {
		var applicationVersions = response.data;
	});
```
The query string parameters are:

* **releaseTypeId** is the release type for which to fetch application versions. Supply a guid of 
zeros (Guid.Empty) to fetch versions for all release types. 
* **skip** and **take** parameters are required, but unused (this is a legacy issue). Just supply 0.
* **page** is the page number to fetch, starting at 1.
* **pageSize** is the number of items in a page.
* **sortKey**. Optional. The key to sort on. The default is the display version. See 
the [Loupe API documentation](http://api.onloupe.com).
* **sortDirection**. Optional. The direction in which to sort. The default is descending.

This is a common pattern across a number of Loupe API data calls that supply paged data.

> Note that the product and application headers are not required for this call.
> If omitted, the application versions returned will not be constrained to a particular
> application, and versions for all applications will be returned (within the
> constraints of the supplied paging).

The details returned are a `GridResult` comprising:

* **data**. A list of the data.
* **total**. The total number of rows available

For the application versions, the data is a list of `ApplicationVersionViewModel` objects, comprisiong:

* **id**. The ID of the Application Version.
* **version**. An `ActionDescriptor` that enapsulates a Loupe link to the actual item, containing properties such as the routing information and title.
* **caption**. The display caption.
* **releaseDate**. Optional. The date the version was released.
* **releaseType**. A string describing the release type (such as debug, internal, or release).
* **promotionLevel**. A string describing the promotion level (such as development, QA, staging, release).
* **releaseNotes**. Details of the title and url of where the release notes are.

### Fetching an Application Version for editing

To fetch a single application version, you need the ID of the Application Version:

```javascript
var url = '/api/ApplicationVersion/Get/' + applicationVersionId;
var settings = {
	url: baseUrl + 'Customers/' + tenantName + url,
	headers: {
		'Authorization': 'Session ' + accessToken,
		'loupe-product': productName,
		'loupe-application': applicationName
	}
};
$.ajax(settings)
	.done(function (response) {
		var version = response.version,
			releaseTypes = response.lists.releaseTypes,
			promotionLevels = response.lists.promotionLevels
			;
	});
```
Note that not only is the application version returned, but also the reference data
required for editing: the release types and promotion levels. The version is
an `ApplicationVersionSaveModel`, comprising:

* **id**. The ID of application version.
* **applicationId**. The ID of the application.
* **caption**. The version caption. The maximum size iss 120.
* **description**. Optional. The description of the version.
* **displayVersion**. The version number to display.
* **promotionLevel**. Optional. The ID of the Promotion Level.
* **releaseDate**. Optional. The date the version was released.
* **releaseNotesUrl**. Details of where the release notes are. The maximum size is 2048.
* **releaseType**. The ID of the Release Type.
* **version**. The actual version number.
* **versionBin**. A single number representation of the version number.

### Updating an Application Version
To save an existing version, you simple send the `ApplicationVersionSaveModel` back 
to the server as a JSON object:

```javascript
var settings = {
	url: baseUrl + 'Customers/' + tenantName + '/api/ApplicationVersion/Put/',
	method: 'PUT',
	data: updatedVersionDetails,
	headers: {
		'Authorization': 'Session ' + accessToken,
		'loupe-product': productName,
		'loupe-application': applicationName
	}
};
$.ajax(settings)
	.done(function (response) {
		// saved OK
	})
	.fail(function (response) {
		if (response.status === 404) {
			// version with that ID not found
		} else if (response.status === 400) {
			// data failed model validation
		}
	});
```

For model validation, the `Version` and `DisplayVersion` must match the standard version number 
format of *Major.Minor.Build.Revision*. If model validation does fail, the failures are returned
in `response.responseJSON.modelState`.

### Creating an Application Version
Creating an application version uses the same details as above, but with the `POST` method and `Post`
action. For example:

```javascript
var settings = {
	url: baseUrl + 'Customers/' + tenantName + '/api/ApplicationVersion/POST/',
	method: 'POST',
	data: updatedVersionDetails,
	headers: {
		'Authorization': 'Session ' + accessToken,
		'loupe-product': productName,
		'loupe-application': applicationName
	}
};
```

Since it's a new version, the ID should just be Guid.Empty. For `versionBin`, this can be left 
at 0, as it's recalculated on the server. The sample project does have a `VersionNumber` object
that can be used in javascript applications if you wish to use the numeric representation 
(eg for sorting). 

#### New Version Reference Data
Creating a new version requires the reference data (release types and promotion levels), so
you can obtain a blank version by calling `GetNew`. This returns the same details as `Get`,
but sets the `Id` to Guid.Empty and uses the current application (in the header) to 
set the `ApplicationId`. 

### Issues for an Application Version
If you are raising and resolving issues in applications, you may need to obtain a list of issues
within a particular application version. There are two API calls for this, one that obtains open issues
and one that obtains closed issues. For example, the following shows how to obtain open issues
for an application version:

```javascript
var url = 'Customers/' + currentTenant + '/api/Issues/OpenForApplication' +
    '?applicationVersionId=' + currentVersion.id + 
    '&take=0&skip=0' + 
    '&page=1&pageSize=10';
var settings = {
	url: baseUrl + url,
	method: 'GET',
	headers: {
		'Authorization': 'Session ' + accessToken
	}
};
$.ajax(settings)
	.done(function (response) {
		var issues = response.data;
	});
```

>> Note that the 'take' and 'skip' parameters are ignored and are only included for legacy reasons.

>> To fetch the closed issues, use the `ClosedForApplication` endpoint.

The returned data is a list of issues, each containing:

* **adddedBy**. Details of who added the user:
  * **email**. The user's email details:
    * **address**. The email address
	* **hash**. The email hash, for gravatar use
  * **id**. The user id
  * **title**. The display title of the user
  * **url**. The client URL used to view the user
* **addedOn**. The date and time the issue was added
* **applicationName**. The name of the application associated with the issue
* **assignedTo**. Details of the user the issue is assigned to (in the same format as addedBy)
* **caption**. Details of the issue caption:
  * **id**. The unique id of the issue
  * **isSuppressed**. Indicates whether or not the issue is suppressed
  * **status**. Descriptive status of the issue (eg Active)
  * **title**. Full title of the issue
  * **url**. The client URL used to display the issue
* **endpoints**. The number of computers this issue affects
* **fixedInVersion**. Details of the version this is fixed in (if the issue is resolved):
  * **bin**. Binary version number
  * **id**. Unique ID of the application version
  * **title**. Version number caption.
  * **url**. The client URL used to view the application version
  * **version**. The actual version number
* **id**. The ID of the issue.
* **lastOccurredOn**. The date and time the issue last occurred
* **occurrences**. The number of event occurrences of the issue
* **productName**. The name of the product associated with the issue
* **selectable**. For UI use only, indicating if the issue is selectable
* **sessions**. The number of sessions within which the issuee occurred
* **status**. Descriptive status of the issue (eg Active)
* **updatedBy**. Details of the user that updated the issue (in the same format as addedBy)
* **updatedOn**. The date and time the issue was updated
* **users**. The number of users affected by this issue

## Removing a User Account
The main administration area of Loupe allows for accounts to be deactivated, they are not actually
removed from the system, catering for users who, perhaps, leave a company while their historic data 
needs to be kept.

With the arrival of GDPR in Europe, users now need the ability to completely remove their account from Loupe.
Note that removal of an account will completely remove that account. Subsequently you will be unable to login
to the Loupe service and all notifications to you from the Loupe service will cease.