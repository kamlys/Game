function fillData(Name, Number) {
    $(".inputProductName").val(Name);
    $(".inputNumber").val(0);
    $(".inputNumber").attr("max", Number);
}

function updaterow(inputName, value) {
    for (var i = 0; i < inputName.length; i++) {
        $("." + inputName[i]).val(value[i]);
    }
    $(".inputID").attr("readonly", true);
}