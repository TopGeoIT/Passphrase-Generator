app = angular.module('passphraseApp', []);

app.controller('generateController', function ($scope, $http, $interval) {
    $scope.apiUrl = '/api/passphrasegen';

    $scope.genEnthropy = function () {

        $http({
            method: 'GET',
            url: $scope.apiUrl + 'bits' + '?bits=' + $("#enthropy_input").val(),
            data: {
                bits: $("#enthropy_input").val()
            }
        })
            .success(function (data, status) {
                if (data.error == null) {
                    console.log(data);
                    $("#texter")[0].innerHTML = data;
                } else {
                    alert("Error " + data.error);
                }
            })
            .error(function (data, status) {
                alert("Error");
            });
    };
    $scope.genBinary = function () {

        $http({
            method: 'POST',
            url: $scope.apiUrl + 'binary',
            data: "\"" + $("#bits_input").val() + "\""
            
        })
            .success(function (data, status) {
                if (data.error == null) {
                    console.log(data);
                    $("#texter")[0].innerHTML = data;
                } else {
                    alert("Error " + data.error);
                }
            })
            .error(function (data, status) {
                alert("Error");
            });
    };
    $scope.genSentence = function () {

        $http({
            method: 'PUT',
            url: $scope.apiUrl + 'sentence',
            data: "\"" + $("#sentence_input").val() + "\""
        })
            .success(function (data, status) {
                if (data.error == null) {
                    console.log(data);
                    $("#texter")[0].innerHTML = data;
                } else {
                    alert("Error " + data.error);
                }
            })
            .error(function (data, status) {
                alert("Error");
            });
    };

    
});