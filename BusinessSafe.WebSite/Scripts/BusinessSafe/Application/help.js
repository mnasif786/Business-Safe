var helpLibraryManager = function () {
        
    $('[id^="help"]').click(function(event){
        event.preventDefault();

        var href = $(this).attr('href');
        var win=window.open(href, '_blank');
        win.focus();
    
    });

} ();