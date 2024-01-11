(function ($) {
    var l = abp.localization.getResource('Enterprise');

    var _mechanismAppService = microClassroom.enterprise.mechanism;
    var _createModal = new abp.ModalManager(abp.appPath + 'MechanismManagement/New');
    var _editModal = new abp.ModalManager(abp.appPath + 'MechanismManagement/Edit');
    var _dataTable = null;

    // 操作方法
    abp.ui.extensions.entityActions.get('mechanism').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('Enterprise.MechanismManagement.Update'),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: function (data) {
                            return abp.auth.isGranted('Enterprise.MechanismManagement.Delete') && abp.currentUser.id !== data.id;
                        },
                        confirmMessage: function (data) {
                            return l('MechanismDeletionConfirmationMessage',data.record.name);
                        },
                        action: function (data) {
                            _mechanismAppService
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
    abp.ui.extensions.tableColumns.get('mechanism').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('mechanism').actions.toArray()
                        }
                    },
                    {
                        title: l('Name'),
                        data: "name"
                    },
                    {
                        title: l('Pinyin'),
                        data: "pinyin"
                    },
                    {
                        title: l('Slogo'),
                        data: "slogo"
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );


    $(function () {
        // 获取列表
        _dataTable = $('#mechanismTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                serverSide: true,
                paging: true,
                order: [[1, "asc"]],
                searching: false,
                scrollX: true,
                ajax: abp.libs.datatables.createAjax(_mechanismAppService.getPagedList),
                columnDefs: abp.ui.extensions.tableColumns.get('mechanism').columns.toArray()
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        $('#NewMechanismButton').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });

})(jQuery);
