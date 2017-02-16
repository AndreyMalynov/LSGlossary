
$(document).ready(function () {
    GetAllWords();

    $("#addWordBtn").click(function (event) {
        event.preventDefault();
        AddWord();
    });
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


function AddWord() {
    var nameOfWord = $('#addWord').val();

    $.ajax({
        url: '/api/values/',
        type: 'POST',
        data: JSON.stringify(nameOfWord),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllWords();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

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
    var strResult = "<table class='table'><th>Name</th><th>Pronunciation</th><th>Definition</th><th>Example</th><th></th>";
    $.each(words, function (index, word) {
        strResult += "<tr" + " id='" + word.Id + "'><td class='name'>" + word.Name + "</td><td class='pronunciation'> " + word.Pronunciation +
            "</td><td class='definition'>" + word.Definition + "</td><td class='example'> " + word.Example + "</td>" +
        //"<td><div class='edit-icon' id='editItem' data-item='" + elementOfList.Id + "' onclick='EditItem(this);' ></div></td>" +
        "<td><div class='delete' onclick='DeleteWord("+ word.Id + ");' ></div></td>"
        + "</tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);
    AddEditEvent();
}

function AddEditEvent() {
    $(function () {
        $('td').click(function (e) { //ловим элемент, по которому кликнули
            var t = e.target || e.srcElement; //получаем название тега
            var elm_name = t.tagName.toLowerCase(); //если это инпут - ничего не делаем
            if (t.className == 'delete') { return false; } 
            if (elm_name == 'input') { return false; }
            var val = $(this).html();
            var code = '<input type="text" id="edit" value="' + val + '" />';
            $(this).empty().append(code); $('#edit').focus();
            $('#edit').blur(function () {
                var val = $(this).val();
                var id = $(this).parent().parent().attr("id");                         
                $(this).parent().empty().html(val);

                SaveEditWord(id, $('#' + id + ' .name').html(), $('#' + id + ' .pronunciation').html(),
                   $('#' + id + ' .definition').html(), $('#' + id + ' .example').html());

            });
        });
    });
    $(window).keydown(function (event) { //ловим событие нажатия клавиши
        if (event.keyCode == 13) { //если это Enter
            $('#edit').blur(); //снимаем фокус с поля ввода
        }
    });
}

function SaveEditWord(id, name, pronunciation, definition, example) {
    //alert(id + '\n' + name + '\n' + pronunciation + '\n' + definition + '\n' + example);
    var editedWord = {
        Id: id,
        Name: name,
        Pronunciation: pronunciation,
        Definition: definition,
        Example: example,
    };
    $.ajax({
        url: '/api/values/' + id,
        type: 'PUT',
        data: JSON.stringify(editedWord),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllWords();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function DeleteWord(id) {
    $.ajax({
        url: '/api/values/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllWords();
        },
        error: function (x, y, z) {
            alert(id + '\n' + y + '\n' + z);
        }
    });
}