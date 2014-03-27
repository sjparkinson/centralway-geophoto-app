'use strict';

var appServices = angular.module("appServices", []);

appServices.factory("PlaceService", function ($rootScope)
{
    var place;

    var update = function (updatedPlace)
    {
        this.place = updatedPlace;

        broadcast(updatedPlace);
    };

    var loadPhotos = function(place)
    {
        $rootScope.$broadcast("PlaceService.LoadPhotos");
    }

    var broadcast = function (place)
    {
        $rootScope.$broadcast("PlaceService.Update");
    };
    
    var listen = function ($scope, callback)
    {
        $scope.$on("PlaceService.Update", function()
        {
            callback();
        });
    };

    return {
        update: update,
        loadPhotos: loadPhotos,
        place: place,
        listen: listen
    };
});