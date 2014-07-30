/**
 * This javascript file checks for the brower/browser tab action.
 * It is based on the file menstioned by Daniel Melo.
 * Reference: http://stackoverflow.com/questions/1921941/close-kill-the-session-when-the-browser-or-tab-is-closed
 */
var validNavigation = false;

function wireUpEvents() {    
    var dont_confirm_leave = 1; //set dont_confirm_leave to 1 when you want the user to be able to leave withou confirmation
    var leave_message = 'You sure you want to leave?'
    function goodbye(e) {
        if (!validNavigation) {
            PageMethods.AbandonSession(MethodRedirect);
            if (dont_confirm_leave !== 1) {
                if (!e) e = window.event;
                //e.cancelBubble is supported by IE - this will kill the bubbling process.
                e.cancelBubble = true;
                e.returnValue = leave_message;                
                //e.stopPropagation works in Firefox.
                if (e.stopPropagation) {
                    e.stopPropagation();
                    e.preventDefault();                   
                }
                //return works for Chrome and Safari
                //alert(leave_message);
                return leave_message;                
            }
        }
    }
   // alert(goodbye);
    window.onbeforeunload = goodbye;
    //    function () {
    //    var x = goodbye;
    //    alert(x);
    //}
       
    
    //    function () {
    //    if (goodbye)
    //    PageMethods.AbandonSession(MethodRedirect);
    //};

    // Attach the event keypress to exclude the F5 refresh
    $(document).bind('keypress', function (e) {
        if (e.keyCode == 116) {
            validNavigation = true;
        }
    });

    // Attach the event click for all links in the page
    $("a").bind("click", function () {
        validNavigation = true;
    });

    // Attach the event submit for all forms in the page
    $("form").bind("submit", function () {
        validNavigation = true;
    });

    // Attach the event click for all inputs in the page
    $("input[type=submit]").bind("click", function () {
        validNavigation = true;
    });

}
function MethodRedirect(res) {
    //alert("home page");
    if (res == 1) {
        window.location = "Default.aspx";
    }
}
// Wire up the events as soon as the DOM tree is ready
$(document).ready(function () {
    wireUpEvents();
});