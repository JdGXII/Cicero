﻿var app = angular.module('ciceroApp', ['ngAnimate']);

app.controller('mainCtrl', function ($scope) {

    var demandante = { nombre: "", apellido: "", dni: "", telefono: "", email: "" }; 
    var demandado = { nombre: "", ruc: "", telefono: "", email: "" };
    var reclamo = { video: "", comentario: "Sin comentario" };
    var solicitud = { seleccion: "", otro: "" };


 

    $scope.demandante = demandante;
    $scope.demandado = demandado;
    $scope.reclamo = reclamo;
    $scope.solicitud = solicitud;


   
});


function previewVideo() {

    var preview = document.querySelector('video');
    var file = document.querySelector('#video').files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}

function previewImage(input_id, display_id) {
    var preview = document.querySelector(display_id);
    var file = document.querySelector(input_id).files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}

