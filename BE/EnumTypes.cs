using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Enum to represent the status on the theory test
    /// </summary>
    public enum Thery_status
    {
        /// <summary>
        /// Passed the test
        /// </summary>
        Passed,
        /// <summary>
        /// Need to do the test
        /// </summary>
        NeedToDo,
        /// <summary>
        /// Failed his last test in less then 24 hours
        /// </summary>
        Hold
    };

    /// <summary>
    /// Enum to represent different types of vehicles (private car, motorcycle, truck, etc.)
    /// </summary>
    public enum Car_type
    {
        /// <summary>
        /// A private car
        /// </summary>
        Private_car,
        /// <summary>
        /// Vehicles with 2 wheels (motorcycle, scooter, etc.)
        /// </summary>
        Two_wheeled_vehicle,
        /// <summary>
        /// Medium weight truck
        /// </summary>
        Medium_truck,
        /// <summary>
        /// A high-weight truck
        /// </summary>
        Heavy_truck
    };

    /// <summary>
    /// Enum to represent the type of gearbox of the vehicle
    /// </summary>
    public enum Gearbox_type
    {
        /// <summary>
        /// For vehicles with manual gearbox
        /// </summary>
        Manual,
        /// <summary>
        ///  For vehicles with automatic gearbox
        /// </summary>
        Automatic
    };

    /// <summary>
    /// Enum for gender representation
    /// </summary>
    public enum Gender
    {
        Female,
        Male
    }

    /// <summary>
    /// Enum's result for different test criteria
    /// </summary>
    public enum Score
    {
        /// <summary>
        /// Bad result. The student has no control over this criterion
        /// </summary>
        Bad = 0,
        /// <summary>
        /// Result fine. The student partially controls this criterion
        /// </summary>
        OK,
        /// <summary>
        /// Good result. The student fully controls this criterion
        /// </summary>
        Good
    }
}
