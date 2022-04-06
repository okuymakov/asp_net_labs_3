let modalhtml = null;
let isFirstShowed = false;

$('#orderModal').on('show.bs.modal', function (event) {
    let button = $(event.relatedTarget)
    let id = button.data('id')
    let modal = $(this)
    modal.find('#ProductId').val(id);
    if (!isFirstShowed) {
        modalhtml = modal.find('.modal-body').html();
        isFirstShowed = true;
    }

    modal.find('#order-form').submit(function (e) {
        e.preventDefault(); 
        let form = $(this);     
        $.ajax({
            type: "POST",
            url: '/order/create',
            data: form.serialize(),
            success: function (data) {
                if (data) {
                    modal.find('.modal-body').html(`Заказ успешно оформлен`);
                    modal.find('.create-order').hide();

                }
            }
        }
        );
    });
});

$('.modal').on('hidden.bs.modal', function () {
    let modal = $(this);
    modal.find('.modal-body').html(modalhtml);
    modal.find('.create-order').show();
});
