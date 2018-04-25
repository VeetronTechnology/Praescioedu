myApp.factory('servicehelper', ['$http', '$resource', function ($http, $resource) {
    var baseUrl = 'asd';
    var routeUrl = 'asd';
    var versionInfo = 'ewq';

    var buildUrl = function (resourceUrl) {
        return resourceUrl.toLowerCase();
    };


    var format = function (text) {
        var arguments = ['v1'];
        if (!text) return text;
        for (var i = 1; i <= arguments.length; i++) {
            var pattern = new RegExp("\\{" + (i - 1) + "\\}", "g");
            text = text.replace(pattern, arguments[i - 1]);
        }
        return text;
    };

    return {
       
        Student: $resource(buildUrl('/student/'), null, {
            checkNotificationIsMarked: {
                method: 'get',
                url: baseUrl + versionInfo + '/student/checknotificationismarked',
                params: { classId: '@classId', productId: '@productId', userId: '@userId', assignmentId: '@assignmentId' }
            },
            checkNotificationIsMarkedS: {
                method: 'post',
                url: buildUrl('/Account/adduseractivitylog'),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            },
            updateIdleTimer: {
                method: 'post',
                url: baseUrl + versionInfo + '/account/updateidletimer/:accountid/token/:refreshtoken/idletype/:idletype/totalIdleTimeInSeconds/:totalIdleTimeInSeconds/machineidentifier/:machineidentifier',
                params: { accountid: '@accountid', refreshtoken: '@refreshtoken', idletype: '@idletype', totalIdleTimeInSeconds: '@totalIdleTimeInSeconds', machineidentifier: '@machineidentifier' }
            }
        }),
    };
}]);