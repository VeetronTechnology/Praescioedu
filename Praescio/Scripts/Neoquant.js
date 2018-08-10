//$("#AdvisorBDMCode").NumericOnly();
//$("#AdvisorBDMCode").ReadOnly();
//$("#ClientDOB").change(function () {
//    $("#ClientDOB").CalcAge('age');
//});

function GetDropdownDataByJson(url, bindField) {
    $.getJSON(url, function (data) {
        var items = "";
        items += "<option> --Select-- </option>";
        $.each(data, function (key, value) {
            items += "<option value='" + key + "'>" + value + "</option>";
        });
        $("#" + bindField).html(items);
    });
}

function close() {
    window.open('', '_parent', '');
    window.close();
}

// Numeric only control handler
$.fn.NumericOnly = function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });
    });
};



$.fn.ReadOnly = function () {
    return this.each(function () {
        $(this).attr('readonly', true);
    });
};

$.fn.CalcAge = function (popCtrl) {
    return this.each(function () {
        var dob = new Date($(this).val());
        var today = new Date();
        var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));
        $('#' + popCtrl).val(age);
        console.log(age);
    });
};

$.fn.AlphaOnly = function () {
    return this.each(function () {
        $(this).on("keydown", function (event) {
            // Ignore controls such as backspace
            var arr = [8, 9, 16, 17, 20, 35, 36, 37, 38, 39, 40, 45, 46, 190];
            //Prevent Space

            if ($(this).attr('preventspace') != 'true') {
                arr.push(32);
            }

            // Allow letters
            for (var i = 65; i <= 90; i++) {
                arr.push(i);
            }

            if (jQuery.inArray(event.which, arr) === -1) {
                event.preventDefault();
            }
        });

        $(this).on("input", function () {
            var regexp = /[^a-zA-Z\s.]/g;
            if ($(this).val().match(regexp)) {
                $(this).val($(this).val().replace(regexp, ''));
            }
        });
    });
};

$.fn.ValidateEmail = function () {
    return this.each(function () {
        $(this).on("blur", function (event) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            var isValid = expr.test($(this).val());
            if (!isValid && $(this).val().length > 0) {
                $(this).val('');
                $(this).focus();
                $.alert('Email id is invalid');
            }
            return isValid;
        });
    });
}

$.fn.AlphaNumericOnly = function () {
    return this.each(function () {
        $(this).on("keydown", function (event) {
            // Ignore controls such as backspace
            var arr = [8, 9, 16, 17, 20, 35, 36, 37, 38, 39, 40, 45, 46, 190];
            //Prevent Space
            if ($(this).attr('preventspace') != 'true') {
                arr.push(32);
            }

            // Allow Numbers
            for (var i = 48; i <= 57; i++) {
                arr.push(i);
            }
            // Allow Numbers
            for (var i = 96; i <= 105; i++) {
                arr.push(i);
            }

            // Allow letters
            for (var i = 65; i <= 90; i++) {
                arr.push(i);
            }


            if (jQuery.inArray(event.which, arr) === -1) {
                event.preventDefault();
            }
        });

        $(this).on("input", function () {
            var regexp = /[^a-zA-Z0-9\s.]/g;
            if ($(this).val().match(regexp)) {
                $(this).val($(this).val().replace(regexp, ''));
            }
        });
    });
};

$.fn.Pikadate = function () {
    return this.each(function () {
        var maxDate = new Date();
        var maxYear = maxDate.getFullYear();
        var minYear = new Date().getFullYear() - 100;

        var picker = new Pikaday(
        {
            field: this,
            firstDay: 1,
            format: "DD/MM/YYYY",
            minDate: new Date(minYear, 0, 1),
            maxDate: maxDate,
            yearRange: [minYear, maxYear + 2]
        });
    });
};


$.fn.Pikadate2 = function () {
    return this.each(function () {
        var picker = new Pikaday(
        {
            field: this,
            firstDay: 1,
            format: "DD/MM/YYYY"
        });
    });
};

//Convert dd/mm/yyyy => mm/dd/yyyy
ConvertDateFormat = function (date) {
  if (date == undefined || date == '') return '';
  /*  date = date.toString();
    if (date != '' && date != null) {
        var initial = date.split(/\//);
        return [initial[1], initial[0], initial[2]].join('/'); //=> 'mm/dd/yyyy'
    }
    else {
        return '';
    }*/

  var dt = moment(date, 'DD/MM/YYYY');
  return dt.format('YYYY-MM-DD HH:mm:ss.SSS Z');
};

CalcAge = function (val, prop) {
    var dob = new Date(val);
    var today = new Date();
    var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));
    return age;
};

//Get Today Date in Given Format
GetTodayDate = function (format) {
    if (format == 'mm/dd/yyyy') {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;//January is 0! 
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        return mm + '/' + dd + '/' + yyyy;
    }
    else {
        return '';
    }
};


$(function () {
    if ($('.datepicker').length) {
        $.validator.methods.date = function (value, element) {
            var dateParts = value.split('/');
            var dateStr = dateParts[2] + '-' + dateParts[1] + '-' + dateParts[0];
            return this.optional(element) || !/Invalid|NaN/.test(new Date(dateStr));
        };
    }


});

GetStopWatchTime = function (startTime) {
  if (startTime != null || startTime != undefined) {
    var tStartDate = moment(startTime, 'YYYY-MM-DD HH:mm:ss.SSS Z');
    var seconds = (new Date() - tStartDate) / 1000;

    var days = Math.floor(seconds / (3600 * 24));
    seconds -= days * 3600 * 24;
    var hrs = Math.floor(seconds / 3600);
    seconds -= hrs * 3600;
    var mnts = Math.floor(seconds / 60);
    seconds -= mnts * 60;

    return days + ":" + hrs + ":" + mnts;
  }
  else {
    return '';
  }
};

GetStopWatchTime2 = function (startTime, endTime) {
  if (startTime != null || startTime != undefined || endTime != null || endTime != undefined) {
    var tStartDate = moment(startTime, 'YYYY-MM-DD HH:mm:ss.SSS Z');
    var tEndTime = moment(endTime, 'YYYY-MM-DD HH:mm:ss.SSS Z');
    var seconds = (tEndTime - tStartDate) / 1000;

    var days = Math.floor(seconds / (3600 * 24));
    seconds -= days * 3600 * 24;
    var hrs = Math.floor(seconds / 3600);
    seconds -= hrs * 3600;
    var mnts = Math.floor(seconds / 60);
    seconds -= mnts * 60;

    return days + ":" + hrs + ":" + mnts;
  }
  else {
    return '';
  }
};