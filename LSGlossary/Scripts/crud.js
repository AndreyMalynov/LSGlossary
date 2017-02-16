
$(document).ready(function () {
    GetAllWords();
    // add events
    //$("#editElementOfList").click(function (event) {
    //    event.preventDefault();
    //    EditElementOfList();
    //});

    //$("#addElementOfList").click(function (event) {
    //    event.preventDefault();
    //    AddElementOfList();
    //});
});


//function AddElementOfList() {
//    var nameOfWord = $('#addWord').val();
//    $.ajax({
//        url: '/api/values/',
//        type: 'POST',
//        data: JSON.stringify(nameOfWord),
//        contentType: "application/json;charset=utf-8",
//        success: function (data) {
//            GetAllWords();
//        },
//        error: function (x, y, z) {
//            alert(x + '\n' + y + '\n' + z);
//        }
//    });
//}

function GetAllWords() {
    $.ajax({
        url: '/api/values',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            WriteResponse(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}


function WriteResponse(words) {
    var strResult = "<table class='table'><th>Name</th><th>Pronunciation</th><th>Definition</th><th>Example</th>";
    $.each(words, function (index, word) {
        strResult += "<tr><td>" + word.Name + "</td><td> " + word.Pronunciation + "</td><td>"
            + word.Definition + "</td><td> " + word.Example + "</td><td>" + "</tr>";
        //"<td><div class='edit-icon' id='editItem' data-item='" + elementOfList.Id + "' onclick='EditItem(this);' ></div></td>" +
        //"<td><div class='delete-icon' id='delItem' data-item='" + elementOfList.Id + "' onclick='DeleteItem(this);' ></div></td>"
        
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);
}