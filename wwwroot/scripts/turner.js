
function koController(){
    var that = this;
 
    that.title = ko.observable("");
    that.description = ko.observable("");
    that.genres = ko.observable("");
    that.year = ko.observable(0);
    that.stars = ko.observable("");
    that.wins = ko.observable("");

    that.titles = ko.observableArray([]);

    that.searchVal = ko.observable('');
    that.searchVal.subscribe(function (newValue) {
        that.title("");
        that.description("");
        that.genres("");
        that.year(0);
        that.stars("");
        that.wins("");
        that.titles.removeAll();
        
        if (newValue != undefined && newValue.length > 2){
            $.get('/api/titles/getTitles',{searchTerm:newValue})
            .done(function (data) {
                that.titles(data);
            });
        }
    });

    that.selectTitle = function(ix){
        var titleVal = that.titles()[ix];
        $.get('/api/titles/getDetail',{title:titleVal})
            .done(function (data) {
                if (data != undefined){
                    var valIn =  JSON.parse(data);
                    that.title(valIn.TitleName);
                    that.description(valIn.Storylines[0].Description);
                    that.genres(valIn.Genres.join());
                    that.year(valIn.ReleaseYear);
            
                    var parts = valIn.Participants;
                    var stars = [];
                    for (var i=0;i<parts.length;i++){
                        var part = parts[i];
            
                        if (part.IsKey && part.IsOnScreen){
                            stars.push(part.Name);
                        }
                    }
                
                    that.stars(stars.join());
            
                    var awards = valIn.Awards;
                    var wins = [];
            
                    if(awards != undefined){
                        for (var i=0;i<awards.length;i++){
                            var award = awards[i];
                            if (award.AwardWon){
                                wins.push(award.Award);
                            }
                        }
                    }
            
                    that.wins(wins.join());
                }       
            }); 
    }

}

var koBoss = null;

$(function(){
    koBoss = new koController();
    ko.applyBindings(koBoss, $('#titles')[0]);
});