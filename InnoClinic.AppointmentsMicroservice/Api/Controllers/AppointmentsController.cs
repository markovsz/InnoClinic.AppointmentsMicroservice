using Api.Enums;
using Api.Extensions;
using Application.Abstractions;
using FluentValidation;
using InnoClinic.SharedModels.DTOs.Appointments.Incoming;
using InnoClinic.SharedModels.DTOs.Appointments.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IValidator<AppointmentIncomingDto> _appointmentIncomingDtoValidator;
        private readonly IValidator<RescheduleAppointmentIncomingDto> _rescheduleAppointmentIncomingDtoValidator;
        private readonly IValidator<AppointmentParameters> _appointmentParametersValidator;
        private readonly IValidator<ScheduleParameters> _scheduleParametersValidator;

        public AppointmentsController(IAppointmentsService appointmentsService, 
            IValidator<AppointmentIncomingDto> appointmentIncomingDtoValidator,
            IValidator<RescheduleAppointmentIncomingDto> rescheduleAppointmentIncomingDtoValidator,
            IValidator<AppointmentParameters> appointmentParametersValidator,
            IValidator<ScheduleParameters> scheduleParametersValidator)
        {
            _appointmentsService = appointmentsService;
            _appointmentIncomingDtoValidator = appointmentIncomingDtoValidator;
            _rescheduleAppointmentIncomingDtoValidator = rescheduleAppointmentIncomingDtoValidator;
            _appointmentParametersValidator = appointmentParametersValidator;
            _scheduleParametersValidator = scheduleParametersValidator;
        }

        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
        [HttpPost]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] AppointmentIncomingDto incomingDto) 
        {
            var validationResult = await _appointmentIncomingDtoValidator.ValidateAsync(incomingDto);
            validationResult.HandleValidationResult();
            var entityId = await _appointmentsService.CreateAsync(incomingDto);
            return Created("", entityId);
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpPost("appointment/{id}/approve")]
        public async Task<IActionResult> ApproveAppointmentAsync(Guid id)
        {
            await _appointmentsService.ApproveAsync(id);
            return NoContent();
        }

        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Doctor)}")]
        [HttpGet("patient/{patientId}/history")]
        public async Task<IActionResult> GetAppointmentsAsync(Guid patientId)
        {
            var entities = await _appointmentsService.GetAsync(patientId);
            return Ok(entities);
        }

        [Authorize(Roles = $"{nameof(UserRole.Doctor)}")]
        [HttpGet("schedule")]
        public async Task<IActionResult> GetAppointmentsScheduleByDoctorAsync([FromQuery] ScheduleParameters parameters)
        {
            var validationResult = await _scheduleParametersValidator.ValidateAsync(parameters);
            validationResult.HandleValidationResult();
            var entities = await _appointmentsService.GetScheduleByDoctorAsync(parameters);
            return Ok(entities);
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpGet("list")]
        public async Task<IActionResult> GetAppointmentsByReceptionistAsync([FromQuery] AppointmentParameters parameters)
        {
            var validationResult = await _appointmentParametersValidator.ValidateAsync(parameters);
            validationResult.HandleValidationResult();
            var entities = await _appointmentsService.GetByReceptionistAsync(parameters);
            return Ok(entities);
        }

        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
        [HttpGet("orderedTimeSlots")]
        public async Task<IActionResult> GetTimeSlotsAsync([FromQuery] TimeSlotParameters parameters)
        {
            var entities = await _appointmentsService.GetTimeSlotsAsync(parameters);
            return Ok(entities);
        }

        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
        [HttpPut("appointment/{id}/reschedule")]
        public async Task<IActionResult> RescheduleAppointmentAsync(Guid id, [FromBody] RescheduleAppointmentIncomingDto incomingDto)
        {
            var validationResult = await _rescheduleAppointmentIncomingDtoValidator.ValidateAsync(incomingDto);
            validationResult.HandleValidationResult();
            await _appointmentsService.RescheduleAsync(id, incomingDto);
            return NoContent();
        }

        [Authorize(Roles = $"{nameof(UserRole.Receptionist)}")]
        [HttpDelete("appointment/{id}")]
        public async Task<IActionResult> CancelAppointmentAsync(Guid id)
        {
            await _appointmentsService.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}
