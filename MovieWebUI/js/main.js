$(function(){

    $('.table-header>ul>li').on('click', function(e){
        const $target = $(e.target);
        
        if ( $target.is('div.table-header > ul > li') && !$target.is('.active')){
            $('.table-header>ul > li.active').removeClass('active');
            $target.addClass('active');

            console.log($target[0].dataset.sectionid);
            
            $('section:visible').toggleClass('hidden');
            $(`#${$target[0].dataset.sectionid}`).toggleClass('hidden');
        }
    });

    $('#movieTable').DataTable( {
        ajax: {
            async: false,
            url: 'https://localhost:7044/api/v1/Movie/movies',
            dataSrc: ''
        },
        columns: [
            { data: 'id' },
            { data: 'title' },
            { data: 'releaseDate' },
        ]
    } );

    $('#userTable').DataTable( {
        ajax: {
            async: false,
            url: 'https://localhost:7044/api/v1/Movie/users',
            dataSrc: ''
        },
        columns: [
            { data: 'id' },
            { data: 'age' },
            { data: 'gender' },
            { data: 'zipCode' },
        ]
    } );


    $('#movie-form').on('submit', function(e) {
        e.preventDefault();

        var $form = $(this)

        let movie = {
            "title" : $form.find("input[name=movie-title]").val(),
            "releaseDate" : $form.find("input[name=movie-date]").val()
        }

        console.log(movie);

        $.ajax({
            type: 'POST',
            async: false,
            contentType: 'application/json; charset=utf-8',
            //dataType: "json",
            url: 'https://localhost:7044/api/v1/Movie/movies',
            data: JSON.stringify(movie),
            success: function(data) {
                console.log("Response Data:");
                console.log(data);
            },
            error: function(err){
                console.log(err);
            }
        })
        
    });


})