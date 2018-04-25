myApp.factory('studentSvc', ['$http', '$resource', 'servicehelper',
      function ($http, $resource, servicehelper) {
          var student = servicehelper.Student;

          var checkNotificationIsMarked = function (param) {
              return student.checkNotificationIsMarked(param);
          }

          return {
              checkNotificationIsMarked: checkNotificationIsMarked
          };
      }]);