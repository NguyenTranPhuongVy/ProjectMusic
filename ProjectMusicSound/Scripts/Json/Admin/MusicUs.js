var app = angular.module('myApp', ['ui.bootstrap']);
app.controller('myCtrl', function ($scope, $http) {
    $http({
        method: "GET",
        url: "/JSonAdmin/MusicUsers"
    }).then(function mySuccess(response) {
        //Giả định
        $scope.filteredTodos = []
            , $scope.currentPage = 1
            , $scope.numPerPage = 12
            , $scope.maxSize = 5;

        //Xac dinh list phan trang
        $scope.makeTodos = function () {
            $scope.list = response.data;
        }
        $scope.makeTodos();

        //Phân trang
        $scope.$watch('currentPage + numPerPage', function () {
            var begin = (($scope.currentPage - 1) * $scope.numPerPage)
                , end = begin + $scope.numPerPage;

            $scope.filteredTodos = $scope.list.slice(begin, end);
        });

        $scope.active = function (id) {
            $http.get("/Admin/MusicsAdmin/Active/" + id)
                .then(function (response) {
                    $scope.list = response.data;
                });
        };

        $scope.option = function (id) {
            $http.get("/Admin/MusicsAdmin/Option/" + id)
                .then(function (response) {
                    $scope.list = response.data;
                });
        };

    }, function myError(response) {
        $scope.list = response.statusText;
    });
});