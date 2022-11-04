var mapOptions = {
    zoomControl: false
}

var map = L.map('map', mapOptions).setView([55.75358828417404, 37.619384763529524], 14);
mapLink = 
    '<a href="http://openstreetmap.org">OpenStreetMap</a>';
L.tileLayer(
    'http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; ' + mapLink + ' Contributors',
    maxZoom: 18,
    }).addTo(map);

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
            repeatMode: false,
        },
        polyline: false,
        rectangle: false,
        circle: false,
        marker: false,
        circlemarker: false
    },
    edit: {
        featureGroup: drawnItems
    }
});

var zoomOptions = {
    zoomInTitle: "Приблизить",
    zoomOutTitle: "Отдалить",
};
var zoom = L.control.zoom(zoomOptions);
zoom.addTo(map);

L.drawLocal.draw.toolbar.actions.title = 'Прекратить рисование';
L.drawLocal.draw.toolbar.actions.text = 'Отмена';
L.drawLocal.draw.toolbar.finish.title = 'Закончить рисование';
L.drawLocal.draw.toolbar.finish.text = 'Готово';
L.drawLocal.draw.toolbar.undo.title = 'Удалить последнюю нарисованную точку';
L.drawLocal.draw.toolbar.undo.text = 'Удалить последнюю точку';
L.drawLocal.draw.toolbar.buttons.polygon = 'Нарисуйте область';

L.drawLocal.draw.handlers.polygon.tooltip.start = 'Кликните на карту для начала рисования';
L.drawLocal.draw.handlers.polygon.tooltip.cont = 'Кликните для продолжения рисования';
L.drawLocal.draw.handlers.polygon.tooltip.end = 'Кликните на первую вершину для окочания';
L.drawLocal.draw.handlers.polyline.error = '<strong>Ошибка:</strong> линии не должны пересекаться!',
L.drawLocal.draw.handlers.polyline.tooltip.start = 'Кликните на карту для начала рисования';
L.drawLocal.draw.handlers.polyline.tooltip.cont = 'Кликните для продолжения рисования';
L.drawLocal.draw.handlers.polyline.tooltip.end = 'Кликните на последнюю вершину для завершения линии';
L.drawLocal.draw.handlers.simpleshape.tooltip.end = 'Отпустите кнопку мыши для окончания рисования';

L.drawLocal.edit.toolbar.actions.save.title = 'Сохранить изменения';
L.drawLocal.edit.toolbar.actions.save.text = 'Сохранить';
L.drawLocal.edit.toolbar.actions.cancel.title = 'Прекратить редактирование, отмена всех изменений';
L.drawLocal.edit.toolbar.actions.cancel.text = 'Отмена';
L.drawLocal.edit.toolbar.actions.clearAll.title = 'Удалить все нарисованные фигуры';
L.drawLocal.edit.toolbar.actions.clearAll.text = 'Удалить всё';
L.drawLocal.edit.toolbar.buttons.edit = 'Редактировать слои';
L.drawLocal.edit.toolbar.buttons.editDisabled = 'Отсутствуют слои для редактирования';
L.drawLocal.edit.toolbar.buttons.remove = 'Удалить слои';
L.drawLocal.edit.toolbar.buttons.removeDisabled = 'Отсутствуют слои для удаления';

L.drawLocal.edit.handlers.edit.tooltip.text = 'Потяните вершину или маркер для изменения объекта';
L.drawLocal.edit.handlers.edit.tooltip.subtext = 'Нажмите "Отмена" для возвращения в исходное состояние';
L.drawLocal.edit.handlers.remove.tooltip.text = 'Кликните на объект для его удаления';

map.addControl(drawControl);


let markers = L.layerGroup().addTo(map);
let polygons = [];

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
    refreshMap();
});

map.on('draw:deleted', function (e) {
    refreshMap();
});

function refreshMap() {
    polygons = [];
    drawnItems.clearLayers();
    markers.clearLayers();
}

function newMarker(lat, lng, title, type) {
    let marker = new L.Marker([lat, lng]);
    marker.setIcon(L.icon({ iconUrl: "/img/" + type + ".png", iconSize: [40, 60] }));
    marker.bindPopup(title).openPopup();
    marker.addTo(markers);
}

function drawObjectsFromPolygons(data, status) {
    $("#calculateBtn").prop('disabled', false);
    $('#loader').hide();
    $("#result").text(data);
    var objects = JSON.parse(data);
    objects.forEach(coords => {
        L.polygon([coords], { color: 'green' }).addTo(drawnItems);
    });
};

function drawObjectsFromMarkers(data, status) {
    $("#calculateBtn").prop('disabled', false);
    $('#loader').hide();
    var markers = JSON.parse(data);
    markers.forEach(marker => {
        newMarker(marker["coordinate"]["lat"], marker["coordinate"]["lng"], marker["text"], marker["type"]);
    });
};

function showError() {
    $('#error_message').html("Произошла ошибка сервера (500). Попробуйте запустить расчёт позднее.");
    $("#error_box").fadeIn(500).delay(3000).fadeOut(500);
    $("#calculateBtn").prop('disabled', false);
    $('#loader').hide();
};

$("#submitForm").submit(function () {
    $.ajax({
        type: "POST",
        url: "/Home/Calculate",
        data: JSON.stringify(polygons),
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: drawObjectsFromMarkers,
        error: showError
    });
    $("#calculateBtn").prop('disabled', true);
    $('#loader').show();
    return false;
});