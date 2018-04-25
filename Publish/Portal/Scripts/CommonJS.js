$.fn.Numeric = function () {
    var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;

    $(document).keydown(function (e) {
        if (e.keyCode == ctrl) keyDown = true;
    }).keyup(function (e) {
        if (e.keyCode == ctrl) keyDown = false;
    });

    $(this).on('keypress', function (e) {
        if (!e) var e = window.event;
        if (e.keyCode > 0 && e.which == 0) return true;
        if (e.keyCode) code = e.keyCode;
        else if (e.which) code = e.which;
        var character = String.fromCharCode(code);
        if (character == '\b' || character == ' ' || character == '\t') return true;
        if (keyDown && (code == vKey || code == Vkey)) return (character);
        else return (/[0-9]$/.test(character));
    }).on('focusout', function (e) {
        var $this = $(this);
        $this.val($this.val().replace(/[^0-9]/g, ''));
    }).on('paste', function (e) {
        var $this = $(this);
        setTimeout(function () {
            $this.val($this.val().replace(/[^0-9]/g, ''));
        }, 5);
    });
};

$.fn.Readonly = function () {
    this.on('cut copy click keypress keyup keydown', function () {
        return false;
    });
};


$.fn.CalculateAge = function (AgeId, delimiter) {
    var dateString = $(this).val();

    var dates = dateString.split(delimiter);
    var d = new Date();

    var userday = dates[0];
    var usermonth = dates[1];
    var useryear = dates[2];

    var curday = d.getDate();
    var curmonth = d.getMonth() + 1;
    var curyear = d.getFullYear();

    var age = curyear - useryear;

    if ((curmonth < usermonth) || ((curmonth == usermonth) && curday < userday)) {
        age--;
    }

    $('#' + AgeId).val(age);
    if (age < 18 || age > 69) {
        alert('please enter age between 18 & 69');
        $(this).val('');
        $('#' + AgeId).val('');
    }
};

