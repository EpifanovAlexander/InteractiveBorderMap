var map = L.map('map').setView([55.75358828417404, 37.619384763529524], 14);
mapLink = 
    '<a href="http://openstreetmap.org">OpenStreetMap</a>';
L.tileLayer(
    'http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; ' + mapLink + ' Contributors',
    maxZoom: 18,
    }).addTo(map);

let polygons = [];

var LeafIcon = L.Icon.extend({
    options: {
        shadowUrl: 
            'http://leafletjs.com/docs/images/leaf-shadow.png',
        iconSize:     [38, 95],
        shadowSize:   [50, 64],
        iconAnchor:   [22, 94],
        shadowAnchor: [4, 62],
        popupAnchor:  [-3, -76]
    }
});

var greenIcon = new LeafIcon({
    iconUrl: 'http://leafletjs.com/docs/images/leaf-green.png'
    });

var drawnItems = new L.FeatureGroup();
map.addLayer(drawnItems);

var drawControl = new L.Control.Draw({
    position: 'topleft',
    draw: {
        polygon: {
            shapeOptions: {
                color: 'purple'
            },
            allowIntersection: false,
            drawError: {
                color: 'red',
                timeout: 1000
            },
            showArea: true,
            metric: false,
            repeatMode: false
        },
        polyline: false,
        rectangle: false,
        circle: false,
        marker: false,
        circlemarker: false,
    },
    edit: {
        featureGroup: drawnItems
    }
});
L.drawLocal.draw.toolbar.buttons.polygon = 'Нарисуйте область';
map.addControl(drawControl);




map.on('draw:edited', function (e) {
    var layers = e.layers;
    layers.eachLayer(function (layer) {
        polygons = layer._latlngs;
    });
});

map.on('draw:created', function (e) {
    var layer = e.layer;
    polygons = layer._latlngs;
    drawnItems.addLayer(layer);
});

map.on('draw:drawstart', function (e) {
    drawnItems.clearLayers();
    polygons = [];
});

map.on('draw:deleted', function (e) {
    polygons = [];
});

function drawObjects(data, status) {
    $("#result").text(data);
    var objects = JSON.parse(data);
    objects.forEach(coords => {
        L.polygon([coords], { color: 'green'}).addTo(drawnItems);
    });
};

$("#SubmitForm").submit(function () {
    $.ajax({
        type: "POST",
        url: "/Home/Calculate",
        data: JSON.stringify(polygons),
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: drawObjects
    });
    return false;
});

