var app = angular.module('myApp', []);
app.controller('myAlbum', function ($scope, $http) {
    $http({
        method: "GET",
        url: "/JsonUsers/NewAlbum"
    }).then(function mySuccess(response) {
        $scope.list = response.data;
    }, function myError(response) {
        $scope.list = response.statusText;

    });
});