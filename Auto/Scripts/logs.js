(function() {
    'use strict';
    var app = angular.module('logsApp', ['ngRoute']);
    //config route
    app.config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider){
        $routeProvider
        .when('/Log/GetMessage', { templateUrl: "PartialsViews/message.html" })
        .when('/Log/SearchMessage', { templateUrl: "PartialsViews/messageSearch.html" })
        .when('/Log/GetLogs', { templateUrl: "PartialsViews/logs.html" })
        .when('/Log/SearchLogs', { templateUrl: "PartialsViews/logsSearch.html" });
        $locationProvider.html5Mode(true);
    }]);

    app.controller("logsCtrl", function ($scope, $http, $route, $location) {
        //------------------------------MESSAGE--------------------------//
        
        $scope.messagePageSize = [10, 20, 50, 100];
        $scope.logsPageSize = [10, 20, 50, 100];
        $scope.selectMessageSize = $scope.messagePageSize[1];
        $scope.selectLogsSize = $scope.logsPageSize[1];
        $scope.pager = '';
        $scope.logsPager = '';
        $scope.messageBool = false;
        $scope.logsBool = false;
        //get message empty
        $scope.GetMessage = function () {
            $location.path("/Log/GetMessage");
            $http({
                method: "GET",
                url: "Log/GetMessage"
            }).then(function (message) {
                $scope.msgs = message.data.msgViewModel;
                $scope.pager = GetPager(message.data.pageinfo.TotalItems, message.data.pageinfo.PageNumber, message.data.pageinfo.PageSize);
            });
        };

        //setPage
        $scope.SetPage = function (totalItems, page, pageSize) {
            $http({
                method: "GET",
                url: "Log/GetMessage",
                params: { totalItems, page, pageSize }
            }).then(function (message) {
                $scope.pager = GetPager(message.data.pageinfo.TotalItems, message.data.pageinfo.PageNumber, message.data.pageinfo.PageSize);
                $scope.msgs = message.data.msgViewModel;
            });
        };

        //sort Message on TimeStamp
        $scope.SortMessageTime = function (totalItems, page, pageSize, order) {
            order === false ? order = true : order = false;
            $scope.messageBool = order;
            if ($("#messageArrow").hasClass('glyphicon-arrow-down')) {
                $("#messageArrow").removeClass('glyphicon-arrow-down').addClass('glyphicon-arrow-up');
            }
            else
                $("#messageArrow").removeClass('glyphicon-arrow-up').addClass('glyphicon-arrow-down');
            $http({
                method: "GET",
                url: "Log/GetMessage",
                params: { totalItems, page, pageSize, order }
            }).then(function (message) {
                $scope.pager = GetPager(message.data.pageinfo.TotalItems, message.data.pageinfo.PageNumber, message.data.pageinfo.PageSize);
                $scope.msgs = message.data.msgViewModel;
            });
        };

        //change message pageSize
        $scope.ChangeMessageSize = function (totalItems, page, pageSize) {
            $("#messageArrow").removeClass('glyphicon-arrow-up').addClass('glyphicon-arrow-down');
            $http({
                method: "GET",
                url: "Log/GetMessage",
                params: { totalItems, page, pageSize }
            }).then(function (message) {
                $scope.msgs = message.data.msgViewModel;
                $scope.pager = '';
                $scope.pager = GetPager(message.data.pageinfo.TotalItems, message.data.pageinfo.PageNumber, message.data.pageinfo.PageSize);
            });
        };

        // search sender id, sort on date
        $scope.SearchSender = function (sender_id) {
            if (sender_id === '' || sender_id === undefined) {
                return;
            }
            $scope.senderId = '';           
            $http({
                method: "GET",
                url: "Log/SearchMessage",
                params: { sender_id }
            }).then(function (sender_id) {
                $scope.senderArr = sender_id.data;
                for (var i = 0; i < $scope.senderArr.length; i++) {
                    $scope.senderArr[i].TimeStamp = parseInt($scope.senderArr[i].TimeStamp.replace(/\D+/g, ""));
                }
                function compareTime(a, b) {
                    if (a.TimeStamp < b.TimeStamp) return -1;
                    if (a.TimeStamp > b.TimeStamp) return 1;
                    return 0;
                }
                $scope.senderArr.sort(compareTime);

                $scope.senderArr.reverse();

                for (var k = 0; k < $scope.senderArr.length; k++) {
                    if ($scope.senderArr[k + 1] !== undefined) {
                        if ($scope.senderArr[k].MessageType === 1 && $scope.senderArr[k + 1].MessageType === 0) {
                            var temp = $scope.senderArr[k];
                            $scope.senderArr[k] = $scope.senderArr[k + 1];
                            $scope.senderArr[k + 1] = temp;
                        }
                    }
                }
                $scope.senderTotalItems = 0;
                $scope.senderPageNumber = 1;
                $scope.senderPageSize = 20;
                for (var j = 0; j < $scope.senderArr.length; j++) {
                    $scope.senderTotalItems++;
                }
                $scope.senderPager = GetPager($scope.senderTotalItems, $scope.senderPageNumber, $scope.senderPageSize);
                $scope.senders = $scope.senderArr.slice($scope.senderPager.startIndex, $scope.senderPager.endIndex + 1);
                $location.path("/Log/SearchMessage");
            }, function error(response) {
                toastr.error("Not Found");
            });
                
        };

        // search sender pager
        $scope.SetSenderPage = function (page) {
            $scope.senderPager = GetPager($scope.senderTotalItems, page, $scope.senderPageSize);
            $scope.senders = $scope.senderArr.slice($scope.senderPager.startIndex, $scope.senderPager.endIndex + 1);
        };

        // reverse search sender data on date
        $scope.SortSearchData = function () {
            $scope.senderArr.reverse();
            $scope.senderPager = GetPager($scope.senderTotalItems, $scope.senderPageNumber, $scope.senderPageSize);
            $scope.senders = $scope.senderArr.slice($scope.senderPager.startIndex, $scope.senderPager.endIndex + 1);
            if ($("#searchArrow").hasClass('glyphicon-arrow-down')) {
                $("#searchArrow").removeClass('glyphicon-arrow-down').addClass('glyphicon-arrow-up');
            }
            else
                $("#searchArrow").removeClass('glyphicon-arrow-up').addClass('glyphicon-arrow-down');
        };
        // refresh message
        $scope.RefreshMessage = function (totalItems, page, pageSize, order) {
            $scope.messageBool = false;
            $("#messageArrow").removeClass('glyphicon-arrow-up').addClass('glyphicon-arrow-down');
            $http({
                method: "GET",
                url: "Log/GetMessage",
                params: { totalItems, page, pageSize, order }
            }).then(function (message) {
                $scope.msgs = message.data.msgViewModel;
                $scope.pager = GetPager(message.data.pageinfo.TotalItems, message.data.pageinfo.PageNumber, message.data.pageinfo.PageSize);
            });
        };

        // back to message
        $scope.BackToMessage = function () {
            $location.path("/Log/GetMessage");
        };
    
        //get message
        $scope.GetMessage();

        //----------------------------LOGS-----------------------//
        //get Logs
        $scope.GetLogs = function () {            
            $http({
                method: "GET",
                url: "Log/GetLogs"
            }).then(function (message) {
                $scope.logs = message.data.logViewModel;
                $scope.logsPager = GetPager(message.data.pageinfo.TotalItems, message.data.pageinfo.PageNumber, message.data.pageinfo.PageSize);
                $location.path("/Log/GetLogs");
            });
        };

        //set Logs Page
        $scope.SetLogsPage = function (totalItems, page, pageSize) {
            $http({
                method: "GET",
                url: "Log/GetLogs",
                params: { totalItems, page, pageSize }
            }).then(function (message) {
                $scope.logsPager = GetPager(message.data.pageinfo.TotalItems, message.data.pageinfo.PageNumber, message.data.pageinfo.PageSize);
                $scope.logs = message.data.logViewModel;
            });
        };

        //change logs size
        $scope.ChangeLogsSize = function (totalItems, page, pageSize) {
            $("#logsArrow").removeClass('glyphicon-arrow-up').addClass('glyphicon-arrow-down');
            $http({
                method: "GET",
                url: "Log/GetLogs",
                params: { totalItems, page, pageSize }
            }).then(function (message) {
                $scope.logsPager = GetPager(message.data.pageinfo.TotalItems, message.data.pageinfo.PageNumber, message.data.pageinfo.PageSize);
                $scope.logs = message.data.logViewModel;
            });
        };

        //sort Message on TimeStamp
        $scope.SortLogsTime = function (totalItems, page, pageSize, order) {
            order === false ? order = true : order = false;
            $scope.logsBool = order;
            if ($("#logsArrow").hasClass('glyphicon-arrow-down')) {
                $("#logsArrow").removeClass('glyphicon-arrow-down').addClass('glyphicon-arrow-up');
            }
            else
                $("#logsArrow").removeClass('glyphicon-arrow-up').addClass('glyphicon-arrow-down');
            $http({
                method: "GET",
                url: "Log/GetLogs",
                params: { totalItems, page, pageSize, order }
            }).then(function (message) {
                $scope.logsPager = GetPager(message.data.pageinfo.TotalItems, message.data.pageinfo.PageNumber, message.data.pageinfo.PageSize);
                $scope.logs = message.data.logViewModel;
            });
        };

        //Get Exception
        $scope.GetException = function (id) {
            $scope.exception = '';
            $http({
                method: "GET",
                url: "Log/GetException",
                params: { id }
            }).then(function (message) {
                $scope.exception = message.data;
            });
        };

        // search message id
        $scope.SearchMessageId = function (messageId) {
            if (messageId === '' || messageId === undefined) {
                return;
            }
            $scope.messageId = '';
            $http({
                method: "GET",
                url: "Log/SearchLogs",
                params: { messageId }
            }).then(function (message) {
                $scope.logsFind = message.data;
                $location.path("/Log/SearchLogs");
            }, function error(response) {
                toastr.error("Not Found");
            });

        };

        // refresh logs
        $scope.RefreshLogs = function (totalItems, page, pageSize, order) {
            $scope.logsBool = false;
            $("#logsArrow").removeClass('glyphicon-arrow-up').addClass('glyphicon-arrow-down');
            $http({
                method: "GET",
                url: "Log/GetLogs",
                params: { totalItems, page, pageSize, order }
            }).then(function (message) {
                $scope.logsPager = GetPager(message.data.pageinfo.TotalItems, message.data.pageinfo.PageNumber, message.data.pageinfo.PageSize);
                $scope.logs = message.data.logViewModel;
            });
        };

        // back to message
        $scope.BackToLogs = function () {
            $location.path("/Log/GetLogs");
        };
        

    });
    
    //------------------------- USER'S ------------------------------//
    app.controller("userCtrl", function ($scope, $http) {
        $scope.buttonName = "Show Messages";
        // get messages on user
        $scope.GetMessage = function (sender_id) {
            if ($scope.buttonName === "Show Messages") {
                $http({
                    method: "GET",
                    url: "Log/SearchMessage",
                    params: { sender_id }
                }).then(function (message) {
                    $scope.senderArr = message.data;
                    for (var i = 0; i < $scope.senderArr.length; i++) {
                        $scope.senderArr[i].TimeStamp = parseInt($scope.senderArr[i].TimeStamp.replace(/\D+/g, ""));
                    }
                    $scope.senderTotalItems = 0;
                    $scope.senderPageNumber = 1;
                    $scope.senderPageSize = 20;
                    for (var j = 0; j < $scope.senderArr.length; j++) {
                        $scope.senderTotalItems++;
                    }
                    $scope.senderPager = GetPager($scope.senderTotalItems, $scope.senderPageNumber, $scope.senderPageSize);
                    $scope.senders = $scope.senderArr.slice($scope.senderPager.startIndex, $scope.senderPager.endIndex + 1);
                    $(".message").show();
                    $scope.buttonName = "Hide Messages";
                }, function error(response) {
                    toastr.error("No messages");
                });
            }
            else {
                $(".message").hide();
                $scope.buttonName = "Show Messages";
            }
        };

        // set page for user's messages
        $scope.SetSenderPage = function (page) {
            $scope.senderPager = GetPager($scope.senderTotalItems, page, $scope.senderPageSize);
            $scope.senders = $scope.senderArr.slice($scope.senderPager.startIndex, $scope.senderPager.endIndex + 1);
        };
    });
    //-------------------------FUNC PAGINATION----------------------//
    function GetPager(totalItems, currentPage, pageSize) {
        // default to first page
        currentPage = currentPage || 1;

        // default page size is 10
        pageSize = pageSize || 10;
        // calculate total pages
        var totalPages = Math.ceil(totalItems / pageSize);

        var startPage, endPage;
        if (totalPages <= 10) {
            // less than 10 total pages so show all
            startPage = 1;
            endPage = totalPages;
        } else {
            // more than 10 total pages so calculate start and end pages
            if (currentPage <= 6) {
                startPage = 1;
                endPage = 10;
            } else if (currentPage + 4 >= totalPages) {
                startPage = totalPages - 9;
                endPage = totalPages;
            } else {
                startPage = currentPage - 5;
                endPage = currentPage + 4;
            }
        }

        // calculate start and end item indexes
        var startIndex = (currentPage - 1) * pageSize;
        var endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);

        // create an array of pages to ng-repeat in the pager control
        var pages = [];
        for (var i = startPage; i <= endPage; i++) {
            pages.push(i);
        }

        // return object with all pager properties required by the view
        return {
            totalItems: totalItems,
            currentPage: currentPage,
            pageSize: pageSize,
            totalPages: totalPages,
            startPage: startPage,
            endPage: endPage,
            startIndex: startIndex,
            endIndex: endIndex,
            pages: pages
        };
    }
    //-----------------------------FILTER-----------------------//
    // filter for Date
    app.filter('Date', function () {
        return function (value) {
            value = parseInt(value.replace(/\D+/g, ""));
            return value;
        };

    });
})();