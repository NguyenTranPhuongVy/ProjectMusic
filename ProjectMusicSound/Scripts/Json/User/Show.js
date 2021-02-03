var app = angular.module('myApp', []);
app.controller('myNewHit', function ($scope, $http) {
    $http({
        method: "GET",
        url: "/JsonUsers/MusicShow"
    }).then(function mySuccess(response) {
        $scope.list = response.data;
    }, function myError(response) {
        $scope.list = response.statusText;
    });
});
//Album mới
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
//Album theo view
app.controller('myView', function ($scope, $http) {
    $http({
        method: "GET",
        url: "/JsonUsers/ViewAlbum"
    }).then(function mySuccess(response) {
        $scope.list = response.data;
    }, function myError(response) {
        $scope.list = response.statusText;

    });
});