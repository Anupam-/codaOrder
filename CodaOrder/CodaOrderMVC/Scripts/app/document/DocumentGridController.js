﻿(function () {
    'use strict';

    function GetDocuments($resource) {

        var promise = $resource("GetDocuments")
        return promise;
    }

    GetDocuments.$inject = ['$resource'];
    angular.module('app').factory('GetDocuments', GetDocuments);

    function DocumentGridController($scope, $location, GetDocuments) {

        $scope.title = 'DocumentGridController';

        var filterBarPlugin = {

            init: function (scope, grid) {

                filterBarPlugin.scope = scope;
                filterBarPlugin.grid = grid;
                $scope.$watch(function () {
                    var searchQuery = "";
                    angular.forEach(filterBarPlugin.scope.columns, function (col) {
                        if (col.visible && col.filterText) {
                            var filterText = (col.filterText.indexOf('*') == 0 ? col.filterText.replace('*', '') : "^" + col.filterText) + ";";
                            searchQuery += col.displayName + ": " + filterText;
                        }
                    });
                    return searchQuery;
                }, function (searchQuery) {

                    filterBarPlugin.scope.$parent.filterText = searchQuery;
                    filterBarPlugin.grid.searchProvider.evalFilter();
                });
            },
            scope: undefined,
            grid: undefined,
        };


        // paging
        //$scope.filterOptions = {
        //    filterText: "",
        //    useExternalFilter: true
        //};
        //$scope.totalServerItems = 0;
        //$scope.pagingOptions = {
        //    pageSizes: [5, 10, 20],
        //    pageSize: 5,
        //    currentPage: 1
        //};
        //$scope.setPagingData = function (data, page, pageSize) {
        //    var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
        //    $scope.documents = pagedData;
        //    $scope.totalServerItems = data.length;
        //    if (!$scope.$$phase) {
        //        $scope.$apply();
        //    }
        //};
        //$scope.getPagedDataAsync = function (pageSize, page, searchText) {
        //    setTimeout(function () {
        //        var data;
        //        if (searchText) {
        //            var ft = searchText.toLowerCase();
        //            GetDocuments.query(function (largeLoad) {
        //                //$scope.documents = data;
        //                data = largeLoad.filter(function (item) {
        //                    return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
        //                });
        //                $scope.setPagingData(data, page, pageSize);
        //            });
        //        } else {
        //            GetDocuments.get(function (largeLoad) {
        //                $scope.setPagingData(JSON.parse(largeLoad.Documents), page, pageSize);
        //            });
        //        }
        //    }, 100);
        //};

        //$scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);

        //$scope.$watch('pagingOptions', function (newVal, oldVal) {
        //    if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
        //        $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.gridOptions.$gridScope.filterText);
        //    }
        //}, true);
        //$scope.$watch('filterOptions', function (newVal, oldVal) {
        //    if (newVal !== oldVal) {
        //        $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        //    }
        //}, true);
        //


        $scope.gridOptions = {

            data: 'documents',
            showColumnMenu: true,
            showFilter: true,
            plugins: [filterBarPlugin],
            //showGroupPanel: true,
            //jqueryUIDraggable: true,
            headerRowHeight: 60, // give room for filter bar
            columnDefs: [
                { field: 'Amount', displayName: 'Сумма', headerCellTemplate: '../template/filterHeaderTemplate' },
                { field: 'DocCode', displayName: 'Код док-та', headerCellTemplate: '../template/filterHeaderTemplate' },
                { field: 'DocDate', displayName: 'Дата док-та', headerCellTemplate: '../template/filterHeaderTemplate' },
                { field: 'OID', displayName: 'ID', headerCellTemplate: '../template/filterHeaderTemplate' },
                { field: 'Comments', displayName: 'Комментарий', headerCellTemplate: '../template/filterHeaderTemplate' }
            ],

            //enablePaging: true,
            showFooter: false,
            //totalServerItems: 'totalServerItems',
            //pagingOptions: $scope.pagingOptions,
            filterOptions: $scope.filterOptions
        }

        $scope.setPagingData = function (data, page, pageSize, searchText) {

            var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
            $scope.documents = pagedData;
            $scope.totalServerItems = data.length;
            if (!$scope.$$phase) {

                $scope.$apply();
            }
        };

        $scope.$on('broadcastGetDocuments', function (event, args) {

            var searchText = filterBarPlugin.scope.$parent.filterText;
            GetDocuments.get(function (jsonData) {

                $scope.setPagingData(JSON.parse(jsonData.Documents), args.currentPage, args.pageSize, searchText);
            });

            //var data;
            //if (searchText) {
            //    var ft = searchText.toLowerCase();
            //    GetDocuments.get(function (largeLoad) {
            //    //GetDocuments.query(function (largeLoad) {
            //        //$scope.documents = data;
            //        data = JSON.parse(largeLoad.Documents).filter(function (item) {
            //            return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
            //        });
            //        $scope.setPagingData(data, args.currentPage, args.pageSize);
            //    });
            //} else {
            //    GetDocuments.get(function (largeLoad) {
            //        $scope.setPagingData(JSON.parse(largeLoad.Documents), args.currentPage, args.pageSize);
            //    });
            //}
            console.log(args)
        });
    }

    DocumentGridController.$inject = ['$scope', '$location', 'GetDocuments'];
    angular.module('app').controller('DocumentGridController', DocumentGridController);
})();