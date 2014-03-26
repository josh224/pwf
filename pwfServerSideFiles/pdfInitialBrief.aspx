<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pdfInitialBrief.aspx.vb" Inherits="pdfInitialBrief" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
    <script src="js/angular120/angular.min.js"></script>
    <script src="js/angular120/angular-route.min.js"></script>
    <script src="lib/angular_plus_extras/angular.js"></script>
        <script src="lib/angular_plus_extras/angular-route.js"></script>
</head>
<body ng-app="myApp">

    <div ng-controller="initController"></div>
        <script>

            var myApp = angular.module("myApp", []);
            function initController(streamRiskRegisterToUserService, $scope, $rootScope, $log) {
                //set some base vars and get data from the server


                GetServerData();

                function GetServerData() {




                    //declare analysis here so available to all children - used for running score etc.
                    $scope.analysis = {};
                    $scope.analysis.show = true;
                    $scope.analysis.score = 0;
                    $scope.analysis.risk = 'low';
                }
            }
            myApp.controller('initController', initController);
            myApp.factory('streamRiskRegisterToUserService', function ($http) {
                var headers = { name: "Test Name", number: "1234", user: "josh jones" };
                var input = JSON.stringify(headers);
                var promise;
                var myService = {
                    async: function () {
                        if (!promise) {
                            // $http returns a promise, which has a then function, which also returns a promise
                            var postData = { projectDetails: input };
                            // The next line gets appended to the URL as params
                            // so it would become a post request to /api/user?id=5
                            var config = { params: { id: '5' } };

                            promise = $http.post('pwfservice.asmx/streamRiskRegisterToUser', postData, config).then(function (response) {
                                // The then function here is an opportunity to modify the response
                                console.log(response);
                                // The return value gets picked up by the then in the controller.
                                return response.data;
                            });
                        }
                        // Return the promise to the controller
                        return promise;
                    }
                };
                return myService;
            });
        </script>
</body>
</html>

