'use strict';

var app = angular.module("appControllers", ["appServices"]);

app.controller("SearchController", function ($scope, $http, PlaceService)
{
    $scope.submitted = false;

    $scope.onSearchSubmit = function()
    {
        $http.get("/api/places?query=" + $scope.query).success(function (data)
        {
            $scope.places = data;

            $scope.submitted = true;

            if (data.length === 1)
            {
                $scope.submitted = false;

                PlaceService.update(data[0]);
            }
        });
    }

    $scope.onPlaceClick = function(selection)
    {
        PlaceService.update(selection);
    }

    $scope.getAddress = function(place)
    {
        if (place.Address.indexOf(place.Name) != -1 || !place.Name)
        {
            return place.Address;
        }
        else
        {
            return place.Name + ", " + place.Address;
        }
    }
});

app.controller("MapController", function ($scope, PlaceService)
{
    var mapOptions =
    {
        center: new google.maps.LatLng(51.508515, -0.1254872),
        zoom: 5
    };

    var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);

    var placeMarker;

    PlaceService.listen($scope, function ()
    {
        var place = PlaceService.place;

        var position = new google.maps.LatLng(place.Location.Latitude, place.Location.Longitude);

        if (placeMarker)
        {
            placeMarker.setMap(null);
        }

        placeMarker = new google.maps.Marker({
            position: position,
            map: map,
            title: place.Address
        });

        map.panTo(position);

        if (place.Types.indexOf("country") != -1)
        {
            map.setZoom(5);
        } else {
            map.setZoom(15);
        }

        google.maps.event.addListener(placeMarker, 'click', function ()
        {
            PlaceService.loadPhotos(place);
        });
    });
});

app.controller("PhotoListController", function ($scope, $http, PlaceService)
{
    $scope.markerClicked = false;

    $scope.photos = null;

    $scope.$on("PlaceService.LoadPhotos", function ()
    {
        var place = PlaceService.place;
        $scope.hideLoading = false;

        if (place.HasPhotos)
        {
            $http.get("/api/photos?reference=" + place.Reference).success(function (data) {
                $scope.photos = data;
                $scope.hideLoading = true;
            });

            $scope.source = "Google Place Photos";
        } else {
            $http.get("/api/photos?latitude=" + place.Location.Latitude + "&longitude=" + place.Location.Longitude).success(function (data)
            {
                $scope.photos = data;
                $scope.hideLoading = true;
            });

            $scope.source = "Flickr";
        }

        $scope.markerClicked = true;
    });
});