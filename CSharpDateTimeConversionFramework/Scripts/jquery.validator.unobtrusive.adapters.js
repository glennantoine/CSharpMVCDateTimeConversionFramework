/*global jQuery:false*/
(function () {
    /*
    * Client-side validation methods for validation attributes defined 
    * in CSharpMVCDateTimeConversionFramework.Models.ValidationAttributes
    */

    //Date Format
    jQuery.validator.addMethod("date", function (value, element) {
        if (value === '') {
            return true;
        }
        var split = value.split('/');
        if (split === null || split.length !== 3) {
            return false;
        }
        var dt = new Date(Date.parse(value));
        return value === '' || (dt.getMonth() + 1 === parseInt(split[0], 10) && dt.getDate() === parseInt(split[1], 10) && dt.getFullYear() === parseInt(split[2], 10));
    });

    jQuery.validator.unobtrusive.adapters.addBool("date");

    //Date Not in Future
    jQuery.validator.addMethod("datenotinfuture", function (value, element) {
        return value === '' || Date.parse(value) < new Date();
    });

    jQuery.validator.unobtrusive.adapters.addBool("datenotinfuture");

    //Date Not in Past
    jQuery.validator.addMethod("datenotinpast", function (value, element) {
        var curDate = new Date();
        curDate.setHours(0);
        curDate.setMinutes(0);
        curDate.setSeconds(0);
        curDate.setMilliseconds(0);
        var valDate = new Date(Date.parse(value));
        return value === '' || valDate >= curDate;
    });

    jQuery.validator.unobtrusive.adapters.addBool("datenotinpast");

    //Time Format
    jQuery.validator.addMethod("time", function (value, element) {
        var matches = value.match(/^(0?[1-9]|1[012])(:[0-5]\d) [APap][mM]$/);
        return value === '' || (matches !== null && matches.length > 0);
    });

    jQuery.validator.unobtrusive.adapters.addBool("time");

    //Equals Attribute
    jQuery.validator.addMethod("equalsattribute", function (value, element, other) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var otherVal = $('[name="' + modelPrefix + other + '"]').val();
        return value === otherVal;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("equalsattribute", "other");

    //Date Greater Than Attribute or Null
    jQuery.validator.addMethod("greaterthanattributeornull", function (value, element, other) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var otherVal = $('[name="' + modelPrefix + other + '"]').val();
        return value === '' || parseInt(value, 10) > parseInt(otherVal, 10);
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("greaterthanattributeornull", "other");

    //Date Greater Than Attribute or Null
    jQuery.validator.addMethod("dategreaterthanattributeornull", function (value, element, params) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var otherVal = $('[name="' + modelPrefix + params.other + '"]').val();
        if (params.allowequal === 'True') {
            return value === '' || Date.parse(value) >= Date.parse(otherVal);
        } else {
            return value === '' || Date.parse(value) > Date.parse(otherVal);
        }
    });

    jQuery.validator.unobtrusive.adapters.add('dategreaterthanattributeornull', ['other', 'allowequal'], function (options) {
        options.rules.dategreaterthanattributeornull = {
            other: options.params.other,
            allowequal: options.params.allowequal
        };
        options.messages.dategreaterthanattributeornull = options.message;
    });

    //Time Greater Than Attribute or Null
    jQuery.validator.addMethod("timegreaterthanattributeornull", function (value, element, other) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var otherVal = $('[name="' + modelPrefix + other + '"]').val();
        var otherMinutes = AB.util.timeStringToMinutes(otherVal);
        var minutes = AB.util.timeStringToMinutes(value);
        return minutes === -1 || minutes > otherMinutes;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("timegreaterthanattributeornull", "other");

    //Time Greater Than or equal attribute
    jQuery.validator.addMethod("timegreaterthanequal", function (value, element, other) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var otherVal = $('[name="' + modelPrefix + other + '"]').val();
        var otherMinutes = AB.util.timeStringToMinutes(otherVal);
        var minutes = AB.util.timeStringToMinutes(value);
        return minutes === -1 || minutes >= otherMinutes;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("timegreaterthanequal", "other");

    //Required If Attribute
    jQuery.validator.addMethod("requiredifattribute", function (value, element, other) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var otherNode = $('[name="' + modelPrefix + other + '"]');
        var isset = value !== '';
        if ($(element).is(':radio,:checkbox')) {
            isset = $('input[name="' + element.name + '"]:checked').length > 0;
        }

        if (otherNode.is(':checkbox')) {
            return !otherNode.prop('checked') || isset;
        }
        else {
            return otherNode.val() === '' || isset;
        }
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("requiredifattribute", "other");

    //Required If Any Attribute
    jQuery.validator.addMethod("requiredifanyattribute", function (value, element, other) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var others = other.split(',');
        var i;
        for (i = 0; i < others.length; i++) {
            var otherNode = $('[name="' + modelPrefix + others[i] + '"]');
            var isValid;
            if (otherNode.is(':checkbox')) {
                isValid = !otherNode.prop('checked') || value !== '';
            }
            else {
                isValid = otherNode.val() === '' || value !== '';
            }
            if (!isValid) {
                return false;
            }
        }
        return true;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("requiredifanyattribute", "other");

    //Required If Not Any Attribute
    jQuery.validator.addMethod("requiredifnotanyattribute", function (value, element, other) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var others = other.split(',');
        var i;
        for (i = 0; i < others.length; i++) {
            var otherNode = $('[name="' + modelPrefix + others[i] + '"]');
            var isValid;
            if (otherNode.is(':checkbox')) {
                isValid = otherNode.prop('checked') || value !== '';
            }
            else {
                isValid = (otherNode.length !== 0 && otherNode.val() !== '') || value !== '';
            }
            if (isValid) {
                return isValid;
            }
        }
        return false;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("requiredifnotanyattribute", "other");

    //Required If Attribute And Not Attribute
    jQuery.validator.addMethod("requiredifattributeandnotattribute", function (value, element, params) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var notNode = $('[name="' + modelPrefix + params.notattribute + '"]');
        if (notNode.is(':radio')) {
            notNode = $('[name="' + modelPrefix + params.other + '"]:checked');
        }
        var notNodeValue;
        if (notNode.is(':checkbox')) {
            notNodeValue = notNode.prop('checked') ? 'True' : 'False';
        }
        else {
            notNodeValue = notNode.val();
        }

        //If the not attribute is set, return true
        if (notNodeValue !== null && notNodeValue !== '') {
            return true;
        }

        var otherNode = $('[name="' + modelPrefix + params.ifattribute + '"]');
        if (otherNode.is(':radio')) {
            otherNode = $('[name="' + modelPrefix + params.ifattribute + '"]:checked');
        }
        var otherNodeValue;
        if (otherNode.is(':checkbox')) {
            otherNodeValue = otherNode.prop('checked') ? 'True' : 'False';
        }
        else {
            otherNodeValue = otherNode.val();
        }
        return otherNodeValue === '' || value !== '';
    });

    jQuery.validator.unobtrusive.adapters.add('requiredifattributeandnotattribute', ['ifattribute', 'notattribute'], function (options) {
        options.rules.requiredifattributeandnotattribute = {
            ifattribute: options.params.ifattribute,
            notattribute: options.params.notattribute
        };
        options.messages.requiredifattributeandnotattribute = options.message;
    });


    //Required If Any Attribute And Not Attribute
    jQuery.validator.addMethod("requiredifattributeandnotattribute", function (value, element, params) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var notNode = $('[name="' + modelPrefix + params.notattribute + '"]');
        if (notNode.is(':radio')) {
            notNode = $('[name="' + modelPrefix + params.other + '"]:checked');
        }
        var notNodeValue;
        if (notNode.is(':checkbox')) {
            notNodeValue = notNode.prop('checked') ? 'True' : 'False';
        }
        else {
            notNodeValue = notNode.val();
        }

        //If the not attribute is set, return true
        if (notNodeValue !== null && notNodeValue !== '') {
            return true;
        }

        var others = params.ifattribute.split(',');
        var i;
        for (i = 0; i < others.length; i++) {
            var otherNode = $('[name="' + modelPrefix + others[i] + '"]');
            var isValid;
            if (otherNode.is(':checkbox')) {
                isValid = !otherNode.prop('checked') || value !== '';
            }
            else {
                isValid = otherNode.val() === '' || value !== '';
            }
            if (!isValid) {
                return false;
            }
        }
        return true;
    });

    jQuery.validator.unobtrusive.adapters.add('requiredifanyattributeandnotattribute', ['ifattribute', 'notattribute'], function (options) {
        options.rules.requiredifattributeandnotattribute = {
            ifattribute: options.params.ifattribute,
            notattribute: options.params.notattribute
        };
        options.messages.requiredifattributeandnotattribute = options.message;
    });

    //Required If Attribute Value Equals
    jQuery.validator.addMethod("requiredifattributevalueequals", function (value, element, params) {
        if (($(element).is(':checkbox') && $(element).is(':checked')) ||
            (!$(element).is(':checkbox') && $(element).val() !== null && $(element).val() !== '') ||
            (typeof (value) !== 'undefined' && value !== '')) {
            return true;
        }
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var otherNode = $('[name="' + modelPrefix + params.other + '"]');
        if (otherNode.is(':radio')) {
            otherNode = $('[name="' + modelPrefix + params.other + '"]:checked');
        }
        var otherNodeValue;
        if (otherNode.is(':checkbox')) {
            otherNodeValue = otherNode.prop('checked') ? 'True' : 'False';
        }
        else {
            otherNodeValue = otherNode.val();
        }
        return otherNodeValue === params.othervalue ? value !== '' : true;
    });

    jQuery.validator.unobtrusive.adapters.add('requiredifattributevalueequals', ['other', 'othervalue'], function (options) {
        options.rules.requiredifattributevalueequals = {
            other: options.params.other,
            othervalue: options.params.othervalue
        };
        options.messages.requiredifattributevalueequals = options.message;
    });

    //Required If Attribute Value Not Equals
    jQuery.validator.addMethod("requiredifattributevaluenotequals", function (value, element, params) {
        if (($(element).is(':checkbox') && $(element).is(':checked')) ||
            (!$(element).is(':checkbox') && $(element).val() !== '') ||
            (typeof (value) !== 'undefined' && value !== '')) {
            return true;
        }
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        var otherNode = $('[name="' + modelPrefix + params.other + '"]');
        if (otherNode.is(':radio')) {
            otherNode = $('[name="' + modelPrefix + params.other + '"]:checked');
        }
        var otherNodeValue;
        if (otherNode.is(':checkbox')) {
            otherNodeValue = otherNode.prop('checked') ? 'True' : 'False';
        }
        else {
            otherNodeValue = otherNode.val();
        }
        return otherNodeValue !== params.othervalue ? value !== '' : true;
    });

    jQuery.validator.unobtrusive.adapters.add('requiredifattributevaluenotequals', ['other', 'othervalue'], function (options) {
        options.rules.requiredifattributevaluenotequals = {
            other: options.params.other,
            othervalue: options.params.othervalue
        };
        options.messages.requiredifattributevaluenotequals = options.message;
    });

    //At Least N Required
    jQuery.validator.addMethod("atleastnrequired", function (value, element, count) {
        var intCount = parseInt(count, 10);
        if ($(element).is(':checkbox')) {
            //Checkbox list
            if ($('[name="' + $(element).attr('name') + '"]:checked').length >= intCount) {
                //Apply additional styles as a checkbox list won't properly accept the validation error style
                AB.validation.removeValidationErrorStyle($(element).closest('table'));
                return true;
            }
            else {
                AB.validation.applyValidationErrorStyle($(element).closest('table'));
                return false;
            }
        }
        else if ($(element).is(':radio')) {
            //Radio button list
            if ($('[name="' + $(element).attr('name') + '"]:checked').length >= intCount) {
                //Apply additional styles as a checkbox list won't properly accept the validation error style
                AB.validation.removeValidationErrorStyle($(element).closest('table,formrow'));
                return true;
            }
            else {
                AB.validation.applyValidationErrorStyle($(element).closest('table,formrow'));
                return false;
            }

        } else {
            //Multi-select
            if ($(element).val().length >= intCount) {
                return true;
            }
            else {
                return false;
            }
        }
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("atleastnrequired", "count");


    jQuery.validator.addMethod("checkboxlistrequired", function (value, element, params) {
        return $(element).closest('tbody').find('input[type="checkbox"]:checked').length > 0;
    });

    jQuery.validator.unobtrusive.adapters.addBool("checkboxlistrequired");


    /**
    * UiDateTime Validation attributes
    */

    //Date Greater Than Attribute or Null
    jQuery.validator.addMethod("uidatetimegreaterthandateattributeornull", function (value, element, params) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        if (typeof params.basepropertyname !== 'undefined') {
            modelPrefix = modelPrefix.substr(0, modelPrefix.indexOf(params.basepropertyname) + params.basepropertyname.length + 1);
        }
        var otherVal = $('[name="' + modelPrefix + params.other + '"]').val();
        if (params.allowequal === 'True') {
            return value === '' || Date.parse(value) >= Date.parse(otherVal);
        } else {
            return value === '' || Date.parse(value) > Date.parse(otherVal);
        }
    });

    jQuery.validator.unobtrusive.adapters.add('uidatetimegreaterthandateattributeornull', ['other', 'allowequal', 'basepropertyname'], function (options) {
        options.rules.uidatetimegreaterthandateattributeornull = {
            other: options.params.other,
            allowequal: options.params.allowequal,
            basepropertyname: options.params.basepropertyname
        };
        options.messages.uidatetimegreaterthandateattributeornull = options.message;
    });


    //Time Greater Than Attribute or Null
    jQuery.validator.addMethod("uidatetimegreaterthantimeattributeornull", function (value, element, params) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        if (typeof params.basepropertyname !== 'undefined') {
            modelPrefix = modelPrefix.substr(0, modelPrefix.indexOf(params.basepropertyname) + params.basepropertyname.length + 1);
        }
        var otherVal = $('[name="' + modelPrefix + params.other + '"]').val();
        var otherMinutes = AB.util.timeStringToMinutes(otherVal);
        var minutes = AB.util.timeStringToMinutes(value);
        if (params.allowequal === 'True') {
            return minutes === -1 || minutes >= otherMinutes;
        } else {
            return minutes === -1 || minutes > otherMinutes;
        }
    });

    jQuery.validator.unobtrusive.adapters.add('uidatetimegreaterthantimeattributeornull', ['other', 'allowequal', 'basepropertyname'], function (options) {
        options.rules.uidatetimegreaterthantimeattributeornull = {
            other: options.params.other,
            allowequal: options.params.allowequal,
            basepropertyname: options.params.basepropertyname
        };
        options.messages.uidatetimegreaterthantimeattributeornull = options.message;
    });

    //Date Not in Future
    jQuery.validator.addMethod("uidatetimenotinfuture", function (value, element) {
        return value === '' || Date.parse(value) < new Date();
    });

    jQuery.validator.unobtrusive.adapters.addBool("uidatetimenotinfuture");

    //Date Not in Past
    jQuery.validator.addMethod("uidatetimenotinpast", function (value, element) {
        var curDate = new Date();
        curDate.setHours(0);
        curDate.setMinutes(0);
        curDate.setSeconds(0);
        curDate.setMilliseconds(0);
        var valDate = new Date(Date.parse(value));
        return value === '' || valDate >= curDate;
    });

    jQuery.validator.unobtrusive.adapters.addBool("uidatetimenotinpast");

    //Date Required
    jQuery.validator.addMethod("uidatetimerequired", function (value, element) {
        return value !== null && value !== '';
    });

    jQuery.validator.unobtrusive.adapters.addBool("uidatetimerequired");

    //Required If Attribute
    jQuery.validator.addMethod("uidatetimerequiredifattribute", function (value, element, params) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        if (typeof params.basepropertyname !== 'undefined') {
            modelPrefix = modelPrefix.substr(0, modelPrefix.indexOf(params.basepropertyname) + params.basepropertyname.length + 1);
        }
        var otherNode = $('[name="' + modelPrefix + params.other + '"]');
        var isset = value !== '';
        if ($(element).is(':radio,:checkbox')) {
            isset = $('input[name="' + element.name + '"]:checked').length > 0;
        }

        if (otherNode.is(':checkbox')) {
            return !otherNode.prop('checked') || isset;
        }
        else {
            return otherNode.val() === '' || isset;
        }
    });

    jQuery.validator.unobtrusive.adapters.add('uidatetimerequiredifattribute', ['other', 'basepropertyname'], function (options) {
        options.rules.uidatetimerequiredifattribute = {
            other: options.params.other,
            basepropertyname: options.params.basepropertyname
        };
        options.messages.uidatetimerequiredifattribute = options.message;
    });

    //Required If Not Attribute
    jQuery.validator.addMethod("uidatetimerequiredifnotattribute", function (value, element, params) {
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        if (typeof params.basepropertyname !== 'undefined') {
            modelPrefix = modelPrefix.substr(0, modelPrefix.indexOf(params.basepropertyname) + params.basepropertyname.length + 1);
        }
        if ($(element).is(':checkbox') && !$(element).prop('checked')) {
            value = '';
        }
        if (value === '' || typeof (value) === 'undefined' || value === null) {
            var otherNode = $('[name="' + modelPrefix + params.other + '"]');
            if (otherNode.length === 0) {
                return value !== '';
            }
            if (otherNode.is(':checkbox')) {
                return otherNode.prop('checked') || value !== '';
            }
            else {
                return otherNode.val() !== '' || value !== '';
            }
        } else {
            return true;
        }
    });

    jQuery.validator.unobtrusive.adapters.add('uidatetimerequiredifnotattribute', ['other', 'basepropertyname'], function (options) {
        options.rules.uidatetimerequiredifnotattribute = {
            other: options.params.other,
            basepropertyname: options.params.basepropertyname
        };
        options.messages.uidatetimerequiredifnotattribute = options.message;
    });

    //Required If Attribute Value Equals
    jQuery.validator.addMethod("uidatetimerequiredifattributevalueequals", function (value, element, params) {
        if (($(element).is(':checkbox') && $(element).is(':checked')) ||
            (!$(element).is(':checkbox') && $(element).val() !== null && $(element).val() !== '') ||
            (typeof (value) !== 'undefined' && value !== '')) {
            return true;
        }
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        if (typeof params.basepropertyname !== 'undefined') {
            modelPrefix = modelPrefix.substr(0, modelPrefix.indexOf(params.basepropertyname) + params.basepropertyname.length + 1);
        }
        var otherNode = $('[name="' + modelPrefix + params.other + '"]');
        if (otherNode.is(':radio')) {
            otherNode = $('[name="' + modelPrefix + params.other + '"]:checked');
        }
        var otherNodeValue;
        if (otherNode.is(':checkbox')) {
            otherNodeValue = otherNode.prop('checked') ? 'True' : 'False';
        }
        else {
            otherNodeValue = otherNode.val();
        }
        return otherNodeValue === params.othervalue ? value !== '' : true;
    });

    jQuery.validator.unobtrusive.adapters.add('uidatetimerequiredifattributevalueequals', ['other', 'othervalue', 'basepropertyname'], function (options) {
        options.rules.uidatetimerequiredifattributevalueequals = {
            other: options.params.other,
            othervalue: options.params.othervalue,
            basepropertyname: options.params.basepropertyname
        };
        options.messages.uidatetimerequiredifattributevalueequals = options.message;
    });


    //Required If Attribute Value Not Equals
    jQuery.validator.addMethod("uidatetimerequiredifattributevaluenotequals", function (value, element, params) {
        if (($(element).is(':checkbox') && $(element).is(':checked')) ||
            (!$(element).is(':checkbox') && $(element).val() !== '') ||
            (typeof (value) !== 'undefined' && value !== '')) {
            return true;
        }
        var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
        if (typeof params.basepropertyname !== 'undefined') {
            modelPrefix = modelPrefix.substr(0, modelPrefix.indexOf(params.basepropertyname) + params.basepropertyname.length + 1);
        }
        var otherNode = $('[name="' + modelPrefix + params.other + '"]');
        if (otherNode.is(':radio')) {
            otherNode = $('[name="' + modelPrefix + params.other + '"]:checked');
        }
        var otherNodeValue;
        if (otherNode.is(':checkbox')) {
            otherNodeValue = otherNode.prop('checked') ? 'True' : 'False';
        }
        else {
            otherNodeValue = otherNode.val();
        }
        return otherNodeValue !== params.othervalue ? value !== '' : true;
    });

    jQuery.validator.unobtrusive.adapters.add('uidatetimerequiredifattributevaluenotequals', ['other', 'othervalue', 'basepropertyname'], function (options) {
        options.rules.uidatetimerequiredifattributevaluenotequals = {
            other: options.params.other,
            othervalue: options.params.othervalue,
            basepropertyname: options.params.basepropertyname
        };
        options.messages.uidatetimerequiredifattributevaluenotequals = options.message;
    });

    //Date Format
    jQuery.validator.addMethod("uidatetimeformatdate", function (value, element) {
        if (value === '') {
            return true;
        }
        var split = value.split('/');
        if (split === null || split.length !== 3) {
            return false;
        }
        var dt = new Date(Date.parse(value));
        return value === '' || (dt.getMonth() + 1 === parseInt(split[0], 10) && dt.getDate() === parseInt(split[1], 10) && dt.getFullYear() === parseInt(split[2], 10));
    });

    jQuery.validator.unobtrusive.adapters.addBool("uidatetimeformatdate");

    //Time Format
    jQuery.validator.addMethod("uidatetimeformattime", function (value, element) {
        var matches = value.match(/^(0?[1-9]|1[012])(:[0-5]\d) [APap][mM]$/);
        return value === '' || (matches !== null && matches.length > 0);
    });

    jQuery.validator.unobtrusive.adapters.addBool("uidatetimeformattime");

    //Equals Attribute
    jQuery.validator.addMethod("uidatetimemaxlimitdate", function (value, element, maxyears) {
        var futureDate = new Date();
        futureDate.setFullYear(futureDate.getFullYear() + parseInt(maxyears,10));

        return value === '' || Date.parse(value) < futureDate;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("uidatetimemaxlimitdate", "maxyears");

} ());

//@ var sourceURL, jqueryValidatorUnobstrusiveAdapters = {js:0};  // Stupid IE workaround
//@ sourceURL = jqueryValidatorUnobstrusiveAdapters.js
