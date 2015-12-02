function fillData(Name, Number) {
    console.log(Name);
    console.log(Number);
    $(".inputProductName").val(Name);
    $(".inputNumber").val(0);
    $(".inputNumber").attr("max", Number);
}

function updaterow(inputName, value) {
    for (var i = 0; i < inputName.length; i++) {
        console.log("." + inputName[i] + " " + value[i + 1]);
        $("." + inputName[i]).val(value[i]);
    }
    $(".inputID").attr("readonly", true);
}