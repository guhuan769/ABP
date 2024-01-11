(function ($) {
    var l = abp.localization.getResource('Enterprise');

    var _courseAppService = microClassroom.enterprise.course;
    var _createModal = new abp.ModalManager(abp.appPath + 'CourseManagement/New');
    var _editModal = new abp.ModalManager(abp.appPath + 'CourseManagement/Edit');
    var _dataTable = null;

    // 操作方法
    abp.ui.extensions.entityActions.get('course').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('Enterprise.CourseManagement.Update'),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: function (data) {
                            return abp.auth.isGranted('Enterprise.CourseManagement.Delete') && abp.currentUser.id !== data.id;
                        },
                        confirmMessage: function (data) {
                            return l('CourseDeletionConfirmationMessage',data.record.name);
                        },
                        action: function (data) {
                            _courseAppService
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
    abp.ui.extensions.tableColumns.get('course').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('course').actions.toArray()
                        }
                    },
                    {
                        title: l('Name'),
                        data: "name"
                    },
                    {
                        title: l('Price'),
                        data: "price"
                    },
                    {
                        title: l('StartAt'),
                        data: "startAt"
                    },
                    {
                        title: l('EndAt'),
                        data: "endAt"
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );


    $(function () {
        // 获取列表
        _dataTable = $('#courseTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                serverSide: true,
                paging: true,
                order: [[1, "asc"]],
                searching: false,
                scrollX: true,
                ajax: abp.libs.datatables.createAjax(_courseAppService.getPagedList),
                columnDefs: abp.ui.extensions.tableColumns.get('course').columns.toArray()
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        $('#NewCourseButton').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });

})(jQuery);
