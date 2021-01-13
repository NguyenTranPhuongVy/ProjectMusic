var app = angular.module('myApp', ['ui.bootstrap']);
app.controller('myCtrl', function ($scope, $http) {
    $http({
        method: "GET",
        url: "/JsonUsers/MusicActive"
    }).then(function mySuccess(response) {
        //Giả định
        $scope.filteredTodos = []
            , $scope.currentPage = 1
            , $scope.numPerPage = 12
            , $scope.maxSize = 5;

        //Xac dinh list phan trang
        $scope.makeTodos = function () {
            $scope.listms = response.data;
        }
        $scope.makeTodos();

        //Phân trang
        $scope.$watch('currentPage + numPerPage', function () {
            var begin = (($scope.currentPage - 1) * $scope.numPerPage)
                , end = begin + $scope.numPerPage;

            $scope.filteredTodos = $scope.listms.slice(begin, end);
        });

        $scope.option = function (id) {
            $http.get("/Musics/Del" + id)
                .then(function (response) {
                    $scope.listms = response.data;
                });
        };

    }, function myError(response) {
        $scope.listms = response.statusText;
    });
});