using DeviceCollectionService.Entity;

namespace DeviceCollectionService.IBLL
{
    public interface IWorkerBLL
    {
        /// <summary>
        /// 修改PLC状态
        /// </summary>
        /// <param name="device"></param>
        /// <param name="plcEntity"></param>
        /// <param name="strArr"></param>
        /// <param name="isRun">检测设备是否运行</param>
        /// <returns></returns>
        Task<bool> PlcUpdateType(DeviceOutResponse device, PlcEntity plcEntity, string[] strArr, bool isRun = true);
    }
}