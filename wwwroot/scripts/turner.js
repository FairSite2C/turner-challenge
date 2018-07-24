function titleDetail(valIn){
    var that = this;
}

function koController(){
    var that = this;
    that.searchVal = ko.observable('');
    that.titles = ko.observableArray([]);

    that.titleDetails = new titleDetail();

    that.searchVal.subscribe(function (newValue) {
        that.titles.removeAll();
        if (newValue.length > 2){
            $.get('/api/titles/getTitles',{searchTerm:newValue})
            .done(function (data) {
                that.titles(data);
            });
        }
    });

    that.selectTitle = function(index){
        var ix=index();
        var titleVal = that.titles()[ix];
        $.get('/api/titles/getDetail',{title:titleVal})
            .done(function (data) {
                that.titleDetail = new titleDetail(data);       
            }); 
    }

}

var koBoss = null;

$(function(){

    $("#searchText").keyup(function(){
        var val = $("#searchText").val();    
        $.get('/api/titles/getTitles',{searchTerm:val})
            .done(function (data) {
                for (var i = 0;i<data.length;i++){
            
                }
            });
    });

    $("#testDetail").click(function(){
        var val = 'Annie Hall';    
        $.get('/api/titles/getDetail',{title:val})
            .done(function (data) {

            });
    });

    koBoss = new koController();
    ko.applyBindings(koBoss, $('#titles')[0]);

});