function initMap() {
    var bounds = new google.maps.LatLngBounds();
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 10,
        center: new google.maps.LatLng(-33.92, 151.25),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    var infowindow = new google.maps.InfoWindow();

    var marker, i;

    for (i = 0; i < dealershipsList.length; i++) {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(dealershipsList[i].Latitude, dealershipsList[i].Longitude),
            map: map
        });
        bounds.extend(marker.position);
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                infowindow.setContent(dealershipsList[i].Name);
                infowindow.open(map, marker);
            }
        })(marker, i));
    }
    var newCenter = new google.maps.LatLng(bounds.getCenter().lat(), bounds.getCenter().lng());
    map.setCenter(newCenter);
}