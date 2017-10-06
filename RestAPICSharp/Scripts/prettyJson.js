$(document).ready(function () {

   
    var ugly = $("#divRes").html();
    var obj = JSON.parse(ugly);
    var pretty = JSON.stringify(obj, undefined, 4);
    $("#ApiResponse").val(pretty);

}
     );