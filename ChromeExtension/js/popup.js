$(function(){

    var description,
        tags;

    $('#logged').hide();
     $.get("http://localhost:8080/api/extension", function (data) {
        if (data.Logged) {
            $('#notLogged').hide();
            $('#loggedAs').text( data.Name);
            chrome.browserAction.setIcon({path: 'icons/48.png'});
            $('body').css({"width":"400","height":"400"});
            $('#logged').show();
        }
        else {
            $('body').css({"width":"400","height":"auto"});
            $('#logged').hide();
            chrome.browserAction.setIcon({path: 'icons/48gray.png'});
            $('#notLogged').show();
        }
    });


    $('#singleFieldTags').tagit({
        // This will make Tag-it submit a single form value, as a comma-delimited field.
        singleField: true,
        tagLimit: 5,
        singleFieldNode: $('#mySingleField')
    });

    $(".tagInputCssHelper").attr("maxlength", 20).focusin(function(){
        $(singleFieldTags).css({
            "border-color": "#66afe9",
            "outline": "0",
            "-webkit-box-shadow": "inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, 0.6)",
            "box-shadow": "inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, 0.6)"}).
            focusout(function(){
        $(singleFieldTags).css({"border":"1px solid #C6C6C6",
        "-webkit-box-shadow": "0px 0px 0px #4195fc",
            "box-shadow": "inset 0 1px 0px #C6C6C6, 0 0 0px #C6C6C6"})
     });
    });

    function getWebsiteName(str){
        var arr = str.split(".");
        if(arr.length == 3){
            return arr[1] + '.' + arr[2];
        }
        return str;
    }
    // Update the relevant fields with the new data
    function setInputsInfo(data) {
        $('#inputWebSite').val(getWebsiteName(data.websiteUrl));
        $('#inputUrl').val(data.fullUrl);
        $('#inputTitle').val(data.title);
        convertToDataURLFavIcon('http://www.google.com/s2/favicons?domain='+data.websiteUrl, function(base64Img){
            $('#inputFavIcon').val(base64Img);
        });
        chrome.tabs.captureVisibleTab(null, {}, function (image) {
            $('#inputImage').val(image);
        });
        description = data.description;
    }

    function validateForm()
    {
        var fields = ["inputUrl", "inputWebSite", "inputTitle"];
        var i, l = fields.length;
        var fieldname,fieldVal;

        for (i = 0; i < l; i++) {
            fieldname = fields[i];
            fieldVal = $("#" +fieldname).val();

            if(/^[\u0430-\u044fa-zA-Z0-9- ,.-:·?=|/#()%_]*$/gmi.test(fieldVal) == false) {
                $('#errorNotifier').text(fieldname.substring(5) +" contains invalid characters like '<,>,$,@...'");
                return false;
            }
            if (fieldVal === "") {
                $('#errorNotifier').text(fieldname.substring(5) + " is required field");
                return false;
            }
        }

        return true;
    }

    function getTags(){
        tags=$('#mySingleField').val();
        var result = [];
        if(tags != undefined && tags != ""){
            var arr = tags.split(",");
            for (var i = 0; i < arr.length; i++) {
                var obj = {};
                obj.Name = arr[i];
                result.push(obj);
            }
            return result;
        }
        return [];
    }

    function getWebsite(){
        var website = {};
        var favIcon = $('#inputFavIcon').val() || "";

        if(favIcon != null){
            website.FaviconBase64String = favIcon;
        }

        website.Name = $('#inputWebSite').val();

        return website;
    }

    $('#send').click(function () {
        var isValid = validateForm();
        if(isValid){
            var data = {};
            data.Title =  $('#inputTitle').val();
            data.Url = $('#inputUrl').val();
            data.WebSite = getWebsite();
            data.SnapshotBase64String =  $('#inputImage').val();
            data.Tags = getTags();
            data.Description = $('#inputDescription').val();

            $.ajax({
                type: "POST",
                url: "http://localhost:8080/api/bookmark",
                data: JSON.stringify(data),
                success: function(data){
                    $('#errorNotifier').hide();
                    $('#successNotifier').text(data).show();
                },
                error: function(data){
                    $('#successNotifier').hide();
                    $('#errorNotifier').text(data.Message).show();
                },
                contentType : "application/json"
            });
        }

    });

    $('#plusBtnSign').click(function(){
        if(description != null  && description != "" && description != undefined){
            $('#inputDescription').val(description);
            $('#addDesciptionParagraph').text('Description added');
        }
        else{
            $('#addDesciptionParagraph').text('The page does not contain description');
        }
    });

    function convertToDataURLFavIcon(url, callback, outputFormat){
        var img = new Image();
        img.crossOrigin = 'Anonymous';
        img.onload = function(){
            var canvas = document.createElement('CANVAS');
            var ctx = canvas.getContext('2d');
            var dataURL;
            canvas.height = this.height;
            canvas.width = this.width;
            ctx.drawImage(this, 0, 0);
            dataURL = canvas.toDataURL(outputFormat);
            callback(dataURL);
            canvas = null;
        };
        img.src = url;
    }

    // Once the DOM is ready...
    window.addEventListener('DOMContentLoaded', function () {
        // ...query for the active tab...
        chrome.tabs.query({
            active: true,
            currentWindow: true
        }, function (tabs) {
            //send message to the content script
            chrome.tabs.sendMessage(
                tabs[0].id,
                {from: 'popup', subject: 'DOMInfo'},
                setInputsInfo);
        });
    });
});
