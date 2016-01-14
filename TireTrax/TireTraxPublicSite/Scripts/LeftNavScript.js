function validateUrl() {

    if (window.location.href.toLowerCase().indexOf("logosetting") > -1) {

        $('.Settings').show();
    }
    else if (window.location.href.toLowerCase().indexOf("bankaccount") > -1) {

        $('.Settings').show();
    }
    else if (window.location.href.toLowerCase().indexOf("creditcard") > -1) {

        $('.Settings').show();
    }
    else if (window.location.href.toLowerCase().indexOf("profile") > -1) {

        $('.Settings').show();
    }

    else if (window.location.href.toLowerCase().indexOf("settings") > -1) {

        $('.PTE').show();
    }
    else if (window.location.href.toLowerCase().indexOf("inventory-load") > -1) {

        $('.Inventory').show();
    }
    else if (window.location.href.toLowerCase().indexOf("lotinfo") > -1) {

        $('.Inventory').show();
    }
    else if (window.location.href.toLowerCase().indexOf("facility") > -1) {

        $('.Inventory').show();
    }
    else if (window.location.href.toLowerCase().indexOf("deliverynotes") > -1) {

        $('.Inventory').show();
    }
    else if (window.location.href.toLowerCase().indexOf("deliveryreceipt") > -1) {

        $('.Inventory').show();
    }
    else if (window.location.href.toLowerCase().indexOf("templates") > -1) {

        $('.Settings').show();
    }
    else if (window.location.href.toLowerCase().indexOf("AccountManagement") > -1) {

        $('.Settings').show();
    }


}



$(document).ready(function () {
    validateUrl();

    $('.sidebar-collapse > ul > li > #link_Settings').bind('onClick', openSettingMenu);
    $('.sidebar-collapse > ul > li > #link_Inventory').bind('onClick', openInventoryMenu);
    $('.sidebar-collapse > ul > li > #link_PTE').bind('onClick', openPTEMenu);

    function openSettingMenu() {
        $('.Inventory').hide();
        $('.Settings').show();
    };

    function openPTEMenu() {

        $('.Inventory').hide();
        $('.Settings').hide();
        $('.ptestandard').show();
    };


    function openInventoryMenu() {

        $('.Settings').hide();
        $('.Inventory').show();
    };
});
