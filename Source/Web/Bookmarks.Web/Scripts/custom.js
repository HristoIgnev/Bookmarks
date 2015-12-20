var resizeBookmarkContainer = function () {
    var windowWidth = window.innerWidth;
    if(windowWidth<992){        
        $('.crop').height(130);
    }
    else {
        var value = (windowWidth / 10) - 50;
        $('.crop').height(value);
    }
}

$(document).ready(resizeBookmarkContainer);
$(window).resize(resizeBookmarkContainer);

