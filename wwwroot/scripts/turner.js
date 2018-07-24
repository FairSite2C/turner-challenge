$(function(){

$.get('/api/titles/getTitles')
    .done(function (data) {
        for (var i = 0;i<data.length;i++){
            alert (data[i]);
        }
    });
});