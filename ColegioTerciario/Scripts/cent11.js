/*
$(document).ready(function () {
    var alumnos = [];
    // limpio storage
    localStorage.removeItem('alumnos');

    $(document).on('click', '.agregar-persona-a-curso', function () {
        $(this).css('pointer-events', 'none').css('color','black');
        alumnos.push($(this).data('id'));
        console.log(alumnos);
        localStorage.setItem('alumnos', JSON.stringify(alumnos));

        //console.log(JSON.parse(localStorage.getItem('alumnos')));
        /*
        if (localStorage.getItem('alumnos')) {
            console.log("EXISTEN ALUMNOS");

            var alumnos = JSON.parse(localStorage.getItem('alumnos'));
            console.log(alumnos);
            //alumnos.push($(this).data('id'));
            //localStorage['alumnos'] = alumnos;

            //console.log(localStorage['alumnos']);
        } else {
            console.log("NO EXISTEN ALUMNOS");
            var alumnos = [];
            alumnos.push($(this).data('id'));
            localStorage['alumnos'] = JSON.stringify(alumnos);
            console.log(localStorage['alumnos']);
        }
        //var personas = localStorage['alumnos'] ? JSON.parse(localStorage['alumnos']) : [];
        //localStorage['alumnos'] = JSON.stringify(personas);

    });
});
*/