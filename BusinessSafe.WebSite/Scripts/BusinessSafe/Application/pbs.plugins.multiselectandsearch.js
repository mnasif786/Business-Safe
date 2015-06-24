PBS.Plugins.MultiSelectAndSearch = function (eventBindingObject, customOptions) {
    var self = this;
    var options = customOptions || {
        searchFilter: function (input) {
            var searchTerm = $(input).val();
            $(input).parent().siblings('div.row').filter(":first").find('ul.available-options').children('li').show().each(function () {
                if ($(this).html().substr(0, searchTerm.length).toLowerCase() != searchTerm.toLowerCase()) {
                    $(this).hide();
                }
            });
            $('selectors.anyMultiSelect ul.selected-options').children('li').removeClass('selected');
        }
    };

    var selectors = {
        anyMultiSelect: options.id === undefined ? ".search-and-multi-select" : "#" + options.id + ".search-and-multi-select"
    };

    var bindOptionsClick = function (sender) {
        $(sender).live('click', function () {
            var option = $(sender);
            var selectedClass = 'selected';
            if (option.hasClass(selectedClass)) {
                option.removeClass(selectedClass);
            } else {
                option.addClass(selectedClass);
            }
        });
    };

    $(selectors.anyMultiSelect + " .available-options").children().each(function () {
        bindOptionsClick(this);
    });

    $(selectors.anyMultiSelect + " input.search").keyup(function () {
        options.searchFilter(this);
    });

    $(selectors.anyMultiSelect + " input.add-to-selected").click(function () {
        var thisButton = $(this).parent();
        thisButton.siblings('ul.available-options').children('li.selected').each(function () {

            thisButton.siblings('select.selected-options').children('option').removeAttr('selected');

            if (thisButton.siblings('select.selected-options').children('option:contains("' + $(this).html() + '")').size() === 0) {
                $(thisButton.siblings('select.selected-options')).append('<option selected="selected" value="' + $(this).attr('data-value') + '">' + $(this).html() + '</option>');

                $(eventBindingObject).trigger('added-item', $(this));
            }
            thisButton.siblings('ul.available-options').children('li').removeClass('selected');
        });
        document.getElementById(thisButton.siblings('select.selected-options').attr('id')).selectedIndex = -1;
    });

    var removeItems = function (removeTrigger) {
        var thisButton = $(removeTrigger).parent();
        thisButton.siblings('select.selected-options').children('option:selected').each(function () {
            removeItem(this);
        });
        thisButton.siblings('ul.available-options').children('li').removeClass('selected');
        thisButton.siblings('select.selected-options').children('option').removeClass('selected');
    };

    var removeItem = function (itemToRemove) {
        var removingItem = $(itemToRemove);
        removingItem.remove();
        $(eventBindingObject).trigger('removed-item', removingItem);
    };

    if (options.removeItemCheck === undefined) {
        $(selectors.anyMultiSelect + " input.remove-from-selected").click(function () {
            removeItems(this);
        });
    }
    else {
        $(selectors.anyMultiSelect + " input.remove-from-selected").click(function () {
            options.removeItemCheck(this, removeItem);
        });
    }

    this.setDisabledOptions = function (ids) {
        var that = this;
        $(ids).each(function (idx, val) {
            that.setDisabledOption(val);
        });
    };

    this.setDisabledOption = function (id) {
        $(selectors.anyMultiSelect + " .available-options").find('li[data-value="' + id + '"]').each(function () {
            $(this).off('click');
            $(this).css('color', 'lightgrey');
            $(this).data('is-disabled', 1);
        });
    };

    this.setEnabledOptions = function (ids) {
        var that = this;
        $(ids).each(function (idx, val) {
            that.setEnabledOption(val);
        });
    };

    this.setEnabledOption = function (id) {
        $(selectors.anyMultiSelect + " .available-options").find('li[data-value="' + id + '"]').each(function () {
            bindOptionsClick(this);
            $(this).css('color', 'black');
            $(this).data('is-disabled', 0);
        });
    };

    this.getSelectedOptions = function () {
        var result = [];
        $(selectors.anyMultiSelect + " .selected-options").children().each(function () {
            result.push($(this).val());
        });
        return result;
    };

    this.getAvailableOptions = function () {
        var result = [];
        $(selectors.anyMultiSelect + " .available-options").children().each(function () {
            result.push($(this).data().value);
        });
        return result;
    };

    this.getDisabledOptions = function () {
        var result = [];
        $(selectors.anyMultiSelect + " .available-options").children().each(function () {
            if ($(this).data().isDisabled) {
                result.push($(this).data().value);
            }
        });
        return result;
    };

    //you need to call this method before a submit because the browser will only detected selected options
    this.addSelectedPropertyToSelectedOptions = function () {
        $(selectors.anyMultiSelect + " .selected-options").children().each(function () {
            $(this).prop('selected', true);
        });
    };


    return self;
};

var SortableListHelper = SortableListHelper || {};

SortableListHelper.getSelectedElements = function (multiSelectList) {
    var selectedElements = [];

    $(multiSelectList).find("li.selected").each(function () {
        selectedElements.push($(this)[0]);
    });

    return selectedElements;
};

//usage  getItemIds($("#selectedHazards"), "data-value");
SortableListHelper.getItemIds = function (multiSelectList, keyAttributeValue) {
    var ids = [];

    $(multiSelectList).find("li").each(function (index) {
        var id = $(this).attr(keyAttributeValue);
        ids.push(id);
    });

    return ids;
};


SortableListHelper.selectDeselectHazard = function (item) {
    var selectedClass = $.fn.multisortable.defaults.selectedClass;

    if ($(item).hasClass(selectedClass)) {
        $(item).removeClass(selectedClass);
    } else {
        $(item).addClass(selectedClass);
    }
};