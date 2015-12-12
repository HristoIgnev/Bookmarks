// Inform the background page that 
// this tab should have a page-action
chrome.runtime.sendMessage({
    from:    'content',
    subject: 'showPageAction'
});

// Listen for messages from the popup
chrome.runtime.onMessage.addListener(function (msg, sender, response) {
    // First, validate the message's structure
    if ((msg.from === 'popup') && (msg.subject === 'DOMInfo')) {

        var metaDesc = asd("description"); //get meta tag with attribute name = description

        // Collect the necessary data
        var domInfo = {
            title:   $('title')[0].text,
            websiteUrl:  document.location.host,
            fullUrl: window.location.href,
            description: metaDesc,
        };

        // Directly respond to the sender (popup),
        // through the specified callback */
        response(domInfo);
    }
});
var asd = function (propery) {
    var metas = document.getElementsByTagName('META');

    for (var i=0; i<metas.length; i++) {
        if(metas[i].getAttribute("name") != null){
            if (metas[i].getAttribute("name").indexOf(propery) > -1) {
                return metas[i].getAttribute("content");
            }
        }
    }
    return "";
}