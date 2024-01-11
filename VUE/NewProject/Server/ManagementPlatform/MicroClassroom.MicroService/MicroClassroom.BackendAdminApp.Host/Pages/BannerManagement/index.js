(function ($) {
    var l = abp.localization.getResource('Enterprise');

    var _bannerAppService = microClassroom.enterprise.banner;
    var _createModal = new abp.ModalManager(abp.appPath + 'BannerManagement/New');
    var _editModal = new abp.ModalManager(abp.appPath + 'BannerManagement/Edit');
    var _dataTable = null;

    // 操作方法
    abp.ui.extensions.entityActions.get('banner').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('Enterprise.BannerManagement.Update'),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: function (data) {
                            return abp.auth.isGranted('Enterprise.BannerManagement.Delete') && abp.currentUser.id !== data.id;
                        },
                        confirmMessage: function (data) {
                            return l('BannerDeletionConfirmationMessage', data.record.title);
                        },
                        action: function (data) {
                            _bannerAppService
                                .remove(data.record.id)
                                .then(function () {
                                    _dataTable.ajax.reload();
                                    abp.notify.success(l('SuccessfullyDeleted'));
                                });
                        },
                    }
                ]
            );
        }
    );

    // 数据列
    abp.ui.extensions.tableColumns.get('banner').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('banner').actions.toArray()
                        }
                    },
                    {
                        title: l('Title'),
                        data: "title"
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );


    $(function () {
        // 获取列表
        _dataTable = $('#bannerTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                serverSide: true,
                paging: true,
                order: [[1, "asc"]],
                searching: false,
                scrollX: true,
                ajax: abp.libs.datatables.createAjax(_bannerAppService.getPagedList),
                columnDefs: abp.ui.extensions.tableColumns.get('banner').columns.toArray()
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        $('#NewBannerButton').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });

})(jQuery);
