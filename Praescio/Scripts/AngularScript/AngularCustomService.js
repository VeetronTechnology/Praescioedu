
var sharedServicesModule = angular.module('sharedServices', []);
sharedServicesModule.service('NetworkService', ['$http', 'config', 'ApiURL', function ($http, config, ApiURL) {

    var urlBase = ApiURL;

    var dataFactory = {};

    dataFactory.get = function (url) {
        return $http.get(urlBase + url, config);
    };

    dataFactory.insert = function (url, obj) {
        return $http.post(urlBase + url, obj, config);
    };

    dataFactory.update = function (url, obj) {
        return $http.put(urlBase + url, obj, config)
    };

    dataFactory.updateactive = function (url) {
        return $http.get(urlBase + url, config)
    };

    dataFactory.delete = function (url) {
        return $http.delete(urlBase + url, config);
    };

    //dataFactory.getByModel = function (url, obj) {
    //    console.log(obj);
    //    //return $http.get(urlBase + url, "{UserId:123}");
    //    return $http({
    //        method: 'GET',
    //        url: urlBase + url,
    //        params: { "user": '{ "UserID": "123" }'}
    //    });
    //};

    dataFactory.insertTransation = function (url, obj) {
        var request = {
            method: 'POST',
            contentType: 'application/json',
            url: url,
            data: obj,
            headers: {
                //transformRequest: angular.identity,
                //'Content-Type': 'application/json'
                //'Content-Type': undefined
                'Content-Type': false
            }
        };
        return $http(request);
    };

    //Function added for upload file from MVC controller
    dataFactory.UploadFiles = function (url, formdata) {
        var request = {
            method: 'POST',
            url: url,
            data: formdata,
            headers: {
                
                'Content-Type': undefined
            }
        };
        return $http(request);
    };

    dataFactory.insertApproval = function (url) {
        return $http.post(urlBase + url);
    };

    return dataFactory;
}]);