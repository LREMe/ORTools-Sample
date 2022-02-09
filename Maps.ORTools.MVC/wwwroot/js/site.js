// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var map;
var markerAllClients = [];
function initialize() {
    var uluru = new google.maps.LatLng(-12.0452384608443, -77.0298671722412);

    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 18,
        center: uluru,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        draggableCursor: 'crosshair',
        mapTypeControl: false,
        panControl: false,
        zoomControl: true,
        scaleControl: false,
        streetViewControl: false,
        panControlOptions: {
            position: google.maps.ControlPosition.RIGHT_TOP
        },
        zoomControlOptions: {
            position: google.maps.ControlPosition.RIGHT_TOP
        }
    });

}


//Function to add all the client's
function addAllClientsPoint(latLng, code) {

    var targetIcon = "icons/blue/number_"+ code.toString() +".png";
    var marker = new google.maps.Marker({
        position: latLng,
        map: map,

        draggable: false,
        icon: targetIcon

    });
    marker.setTitle(code.toString());
    markerAllClients.push(marker);
}


$(document).ready(function () {

    initialize();
    viewPoints();
});