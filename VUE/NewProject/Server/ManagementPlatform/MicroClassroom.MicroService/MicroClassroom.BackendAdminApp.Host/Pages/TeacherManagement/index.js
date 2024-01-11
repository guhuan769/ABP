(function ($) {
    var l = abp.localization.getResource('Enterprise');

    var _teacherAppService = microClassroom.enterprise.teacher;
    var _createModal = new abp.ModalManager(abp.appPath + 'TeacherManagement/New');
    var _editModal = new abp.ModalManager(abp.appPath + 'TeacherManagement/Edit');
    var _dataTable = null;

    // 操作方法
    abp.ui.extensions.entityActions.get('teacher').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('Enterprise.TeacherManagement.Update'),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: function (data) {
                            return abp.auth.isGranted('Enterprise.TeacherManagement.Delete') && abp.currentUser.id !== data.id;
                        },
                        confirmMessage: function (data) {
                            return l('TeacherDeletionConfirmationMessage',data.record.name);
                        },
                        action: function (data) {
                            _teacherAppService
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
    abp.ui.extensions.tableColumns.get('teacher').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('teacher').actions.toArray()
                        }
                    },
                    {
                        title: l('Name'),
                        data: "name"
                    },
                    {
                        title: l('Introduce'),
                        data: "introduce"
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );


    $(function () {
        // 获取列表
        _dataTable = $('#teacherTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                serverSide: true,
                paging: true,
                order: [[1, "asc"]],
                searching: false,
                scrollX: true,
                ajax: abp.libs.datatables.createAjax(_teacherAppService.getPagedList),
                columnDefs: abp.ui.extensions.tableColumns.get('teacher').columns.toArray()
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        $('#NewTeacherButton').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });

})(jQuery);
