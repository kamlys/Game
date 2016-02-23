function fillData(Name, Number) {
    $(".inputProductName").val(Name);
    $(".inputNumber").val(0);
    $(".inputNumber").attr("max", Number);
}

function updaterow(inputName, value) {
    for (var i = 0; i < inputName.length; i++) {
        if (inputName[i].toLowerCase().indexOf("drop") >= 0) {
            $("." + inputName[i]).siblings(".select-dropdown").val(value[i]);
            $("." + inputName[i]).siblings(".select-dropdown").attr({ value: value[i] });
            $("." + inputName[i]).val(value[i]);
        }
        else {
            $("." + inputName[i]).val(value[i]);
        }
    }
    $(".inputID").attr("readonly", true);
} 