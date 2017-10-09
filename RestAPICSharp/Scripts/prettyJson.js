$(document).ready(function () {
   
    var ugly = $("#divRes").html();
    
    if (ugly != "") {
        var obj = JSON.parse(ugly);
        var pretty = JSON.stringify(obj, undefined, 4);
        $("#ApiResponse").val(pretty);
    }
  
    var req = $("#ApiRequest").val();

    if (req != "") {
        var objReq = JSON.parse(req);
        var prettyReq = JSON.stringify(objReq, undefined, 4);
        $("#ApiRequest").val(prettyReq);
    }
}
     );