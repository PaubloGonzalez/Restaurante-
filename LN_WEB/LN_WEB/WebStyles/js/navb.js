
<script>
    $(document).ready(function(){
        $(".add-menu-item").click(function(){
            $(this).toggleClass("open");
            $(this).next(".submenu").slideToggle();
        });
    });
</script>
