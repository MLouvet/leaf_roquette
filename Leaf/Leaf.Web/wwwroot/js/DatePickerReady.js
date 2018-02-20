// This will make every input element with the type "date" into a DatePicker element
if (!Modernizr.inputtypes.date) {
    $(function () {
        $("input[type='date']")
            .datepicker()
            .get(0)
            .setAttribute("type", "text");
    })
}