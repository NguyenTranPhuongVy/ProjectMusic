var app = angular.module('myApp', []);
app.controller('myCtrl', function ($scope, $http) {
    $http({
        method: "GET",
        url: "/JsonUsers/MusicShow"
    }).then(function mySuccess(response) {
        $scope.list = response.data;
    }, function myError(response) {
        $scope.list = response.statusText;
    });
});