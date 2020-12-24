var app = angular.module('myApp', []);
app.controller('myCtrl', function ($scope, $http) {
    $http.get("/JSonAdmin/UserList")
        .then(function (response) {
            // First function handles success
            $scope.list = response.data;
        }, function (response) {
            // Second function handles error
            $scope.list = "Đã Có Lỗi Xảy Ra";
        });
});