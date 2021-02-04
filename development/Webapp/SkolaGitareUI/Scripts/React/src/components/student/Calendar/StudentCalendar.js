/* eslint-disable max-classes-per-file */
/* eslint-disable react/no-unused-state */
import * as React from 'react';
import Paper from '@material-ui/core/Paper';
import { ViewState, EditingState } from '@devexpress/dx-react-scheduler';
import {
    Scheduler,
    Toolbar,
    MonthView,
    WeekView,
    ViewSwitcher,
    Appointments,
    AppointmentTooltip,
    AppointmentForm,
    EditRecurrenceMenu,
    DateNavigator,
    Resources

} from '@devexpress/dx-react-scheduler-material-ui';
import { connectProps } from '@devexpress/dx-react-core';
import { KeyboardDateTimePicker, MuiPickersUtilsProvider } from '@material-ui/pickers';
import MomentUtils from '@date-io/moment';
import { withStyles } from '@material-ui/core/styles';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';
import Fab from '@material-ui/core/Fab';
import IconButton from '@material-ui/core/IconButton';
import AddIcon from '@material-ui/icons/Add';
import TextField from '@material-ui/core/TextField';
import Close from '@material-ui/icons/Close';
import CalendarToday from '@material-ui/icons/CalendarToday';
import Group from '@material-ui/icons/Group';
import Create from '@material-ui/icons/Create';
import { teal, lightBlue } from '@material-ui/core/colors';
import Popper from "@material-ui/core/Popper";
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormHelperText from '@material-ui/core/FormHelperText';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';


import Autocomplete from '@material-ui/lab/Autocomplete';


export const appointments = [
    {
        title: 'Website Re-Design Plan',
        startDate: new Date(2021, 0, 12, 9, 35),
        endDate: new Date(2021, 0, 12, 11, 30),
        id: 0,
        location: 'Room 1',
        request: 1,
        duration: 2,
        students: [{
            "StudentId": "student5",
            "Name": "student5",
            "Surname": "student5ic",
            "Email": "student5@gmail.com",
            "PasswordHash": "svdgbsfbfvfsdgfdg894ur",
            "PhoneNumber": "111331144484938474111",
            "Verified": "1",
            "ActiveDate": "",
            "MembershipTypeId": "type1",
        }]
    }, {
        title: 'Book Flights to San Fran for Sales Trip',
        startDate: new Date(2921, 0, 12, 12, 11),
        endDate: new Date(2021, 0, 12, 13, 0),
        id: 1,
        location: 'Room 1',
        request: 1,
        duration: 1


    }, {
        title: 'Install New Router in Dev Room',
        startDate: new Date(2021, 0, 14, 14, 30),
        endDate: new Date(2021, 0, 14, 15, 35),
        id: 2,
        location: 'Room 2',
        request: 0,
        duration: 1
    }, {
        title: 'Approve Personal Computer Upgrade Plan',
        startDate: new Date(2021, 0, 14, 10, 0),
        endDate: new Date(2021, 0, 14, 11, 0),
        id: 3,
        location: 'Room 2',
        request: 0,
        duration: 1

    },];

let mockStudents = [
    {
        "StudentId": "student1",
        "Name": "student1",
        "Surname": "student1ic",
        "Email": "student1@gmail.com",
        "PasswordHash": "svdvfsdgfdg894ur",
        "PhoneNumber": "11133114444111",
        "Verified": "1",
        "ActiveDate": "",
        "MembershipTypeId": "type1"
    },
    {
        "StudentId": "student2",
        "Name": "student2",
        "Surname": "student2ic",
        "Email": "student2@gmail.com",
        "PasswordHash": "svdfdbdfbvfsdgfdg894ur",
        "PhoneNumber": "11133114444111",
        "Verified": "1",
        "ActiveDate": "",
        "MembershipTypeId": "type1"
    },
    {
        "StudentId": "student3",
        "Name": "student3",
        "Surname": "student3ic",
        "Email": "student3@gmail.com",
        "PasswordHash": "svfvddvfsdgfdg894ur",
        "PhoneNumber": "11133114444111",
        "Verified": "1",
        "ActiveDate": "",
        "MembershipTypeId": "type1"
    },
    {
        "StudentId": "student4",
        "Name": "student4",
        "Surname": "student4ic",
        "Email": "student4@gmail.com",
        "PasswordHash": "srfgvdvfsdgfdg894ur",
        "PhoneNumber": "111334423114444111",
        "Verified": "1",
        "ActiveDate": "",
        "MembershipTypeId": "type1"
    },
    {
        "StudentId": "student5",
        "Name": "student5",
        "Surname": "student5ic",
        "Email": "student5@gmail.com",
        "PasswordHash": "svdgbsfbfvfsdgfdg894ur",
        "PhoneNumber": "111331144484938474111",
        "Verified": "1",
        "ActiveDate": "",
        "MembershipTypeId": "type1"
    },
    {
        "StudentId": "student6",
        "Name": "student6",
        "Surname": "student6ic",
        "Email": "student6@gmail.com",
        "PasswordHash": "svdvfsdgfdg894ur",
        "PhoneNumber": "11133232333114444111",
        "Verified": "1",
        "ActiveDate": "",
        "MembershipTypeId": "type1"
    },
    {
        "StudentId": "student7",
        "Name": "student7",
        "Surname": "student7ic",
        "Email": "student7@gmail.com",
        "PasswordHash": "svjzjzjztdvfsdgfdg894ur",
        "PhoneNumber": "11133114449023849234111",
        "Verified": "1",
        "ActiveDate": "",
        "MembershipTypeId": "type1"
    }

];


const resources = [{
    fieldName: 'request',
    title: 'request',
    instances: [
        { id: 0, text: 'Termin', color: lightBlue[400] },
        { id: 1, text: 'Zahtjev', color: lightBlue[100] },
    ],
}];
const containerStyles = theme => ({
    container: {

        padding: 0,
        paddingBottom: theme.spacing(0),

    },
    divContainer: {
        width: '600px',
    },
    overlay: {
        width: '10%',
        height: '600px'

    },
    content: {
        padding: theme.spacing(2),
        paddingTop: 0,
    },
    header: {
        overflow: 'hidden',
        paddingTop: theme.spacing(0.5),
    },
    closeButton: {
        float: 'right',
    },
    buttonGroup: {
        display: 'flex',
        justifyContent: 'flex-end',
        padding: theme.spacing(0, 2),
    },
    button: {
        marginRight: theme.spacing(2),
    },
    picker: {
        marginRight: theme.spacing(2),
        '&:last-child': {
            marginRight: 0,
        },
        width: '50%',
    },
    wrapper: {
        display: 'flex',
        justifyContent: 'space-between',
        padding: theme.spacing(1, 0),
    },
    icon: {
        margin: theme.spacing(2, 0),
        marginRight: theme.spacing(2),
    },
    textField: {
        width: '100%',
    },
    popper: {
        marginTop: '',
        transform: 'scale(0.9,1)'
    }
});

class AppointmentFormContainerBasic extends React.PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            appointmentChanges: {},
        };

        this.getAppointmentData = () => {
            const { appointmentData } = this.props;
            return appointmentData;
        };
        this.getAppointmentChanges = () => {
            const { appointmentChanges } = this.state;
            return appointmentChanges;
        };

        this.changeAppointment = this.changeAppointment.bind(this);
        this.commitAppointment = this.commitAppointment.bind(this);
    }

    changeAppointment({ field, changes }) {

        console.log('changeAppointment');
        console.log(field);
        console.log(changes);
        const nextChanges = {
            ...this.getAppointmentChanges(),
            [field]: changes
        };
        this.setState({
            appointmentChanges: nextChanges,

        });
    }

    commitAppointment(type) {
        console.log('commitApointment');
        console.log(type);

        const { commitChanges } = this.props;
        const appointment = {
            ...this.getAppointmentData(),
            ...this.getAppointmentChanges(),
        };
        if (type === 'deleted') {
            commitChanges({ [type]: appointment.id });
        } else if (type === 'changed') {
            commitChanges({ [type]: { [appointment.id]: appointment } });
        } else {
            commitChanges({ [type]: appointment });
        }
        this.setState({
            appointmentChanges: {},
        });

    }

    render() {
        const {
            classes,
            visible,
            visibleChange,
            appointmentData,
            cancelAppointment,
            target,
            onHide,
        } = this.props;
        const { appointmentChanges } = this.state;

        const displayAppointmentData = {
            ...appointmentData,
            ...appointmentChanges,
        };

        const isNewAppointment = appointmentData.id === undefined;
        const applyChanges = isNewAppointment
            ? () => this.commitAppointment('added')
            : () => this.commitAppointment('changed');

        const textEditorProps = field => ({
            variant: 'outlined',
            onChange: ({ target: change }) => this.changeAppointment({
                field: [field], changes: change.value,
            }),
            value: displayAppointmentData[field] || '',
            label: field[0].toUpperCase() + field.slice(1),
            className: classes.textField,
        });


        const pickerEditorProps = field => ({
            className: classes.picker,
            // keyboard: true,
            ampm: false,
            value: displayAppointmentData[field],
            onChange: date => this.changeAppointment({
                field: [field], changes: date ? date.toDate() : new Date(displayAppointmentData[field]),
            }),
            inputVariant: 'outlined',
            format: 'DD/MM/YYYY HH:mm',
            onError: () => null,
        })

        const datepickerEditorProps = field => ({
            ampm: false,
            value: displayAppointmentData[field] || 1,
            onChange: duration => {
                console.log('dateonChange');
                console.log(duration);
                this.changeAppointment({
                    field: [field], changes: duration.target.value,
                })
            },
            onError: () => null,
        });

        const studentPickerProps = field => ({
            value: displayAppointmentData[field],
            onChange: (event, values) => this.changeAppointment({
                field: [field], changes: values,
            }),
        });

        const cancelChanges = () => {
            this.setState({
                appointmentChanges: {},
            });
            visibleChange();
            cancelAppointment();
        };

        return (
            <AppointmentForm.Overlay
                visible={visible}
                target={target}

                onHide={onHide}
                className={classes.overlay}

            >
                <div className={classes.divContainer}>
                    <div className={classes.header}>
                        <IconButton
                            className={classes.closeButton}
                            onClick={cancelChanges}
                        >
                            <Close color="action" />
                        </IconButton>
                    </div>

                    <div className={classes.content}>
                        <div className={classes.wrapper}>
                            <Create className={classes.icon} color="action" />
                            <TextField
                                {...textEditorProps('title')}
                            />
                        </div>
                        <div className={classes.wrapper} style={{ display: 'block' }}>
                            <CalendarToday className={classes.icon} color="action" />
                            <MuiPickersUtilsProvider utils={MomentUtils}>
                                <KeyboardDateTimePicker
                                    label="Start Date"
                                    {...pickerEditorProps('startDate')}
                                />

                            </MuiPickersUtilsProvider>
                            <FormControl variant="outlined" className={classes.formControl}>
                                <InputLabel id="select-outlined-label">Trajanje</InputLabel>
                                <Select
                                    labelId="select-outlined-label"
                                    id="select-outlined"
                                    defaultValue={1}
                                    label="Trajane"
                                    {...datepickerEditorProps('duration')}

                                >
                                    <MenuItem value={1}>1 h</MenuItem>
                                    <MenuItem value={2}>2 h</MenuItem>
                                    <MenuItem value={3}>3 h</MenuItem>
                                </Select>
                            </FormControl>

                        </div>
                        <div className={classes.wrapper} >
                            <Group className={classes.icon} color="action" />
                            <div className={classes.textField}>
                                <Autocomplete
                                    classes={
                                        {
                                            popper: classes.popper,
                                            paper: classes.popper
                                        }}
                                    multiple
                                    id="tags-outlined"
                                    options={mockStudents}
                                    getOptionLabel={(student) => student.Name + ' ' + student.Surname}
                                    filterSelectedOptions
                                    renderInput={(params) => (
                                        <TextField
                                            {...params}
                                            variant="outlined"
                                            placeholder="Izaberite učenike"
                                        />
                                    )}
                                    {...studentPickerProps('students')}

                                />
                            </div>
                        </div>
                    </div>
                    <div className={classes.buttonGroup}>
                        {!isNewAppointment && (
                            <Button
                                variant="outlined"
                                color="secondary"
                                className={classes.button}
                                onClick={() => {
                                    visibleChange();
                                    this.commitAppointment('deleted');
                                }}
                            >
                                Obriši
                            </Button>
                        )}
                        <Button
                            variant="outlined"
                            color="primary"
                            className={classes.button}
                            onClick={() => {
                                visibleChange();
                                applyChanges();
                            }}
                        >
                            {isNewAppointment ? 'Dodaj' : 'Spremi'}
                        </Button>
                    </div>
                </div>
            </AppointmentForm.Overlay>
        );
    }
}

const AppointmentFormContainer = withStyles(containerStyles, { name: 'AppointmentFormContainer' })(AppointmentFormContainerBasic);

const styles = theme => ({
    addButton: {
        position: 'absolute',
        bottom: theme.spacing(1) * 3,
        right: theme.spacing(1) * (12),
    },

});

/* eslint-disable-next-line react/no-multi-comp */
class Kalendar extends React.PureComponent {
    constructor(props) {
        super(props);
        let dat = new Date();

        this.state = {
            data: appointments,
            currentDate: dat.getFullYear() + '-' + dat.getMonth() + 1 + '-' + dat.getDate(),
            confirmationVisible: false,
            editingFormVisible: false,
            deletedAppointmentId: undefined,
            editingAppointment: undefined,
            previousAppointment: undefined,
            addedAppointment: {},
            startDayHour: 9,
            endDayHour: 20,
            isNewAppointment: false,
        };
        console.log(this.currentDate);
        this.toggleConfirmationVisible = this.toggleConfirmationVisible.bind(this);
        this.commitDeletedAppointment = this.commitDeletedAppointment.bind(this);
        this.toggleEditingFormVisibility = this.toggleEditingFormVisibility.bind(this);

        this.commitChanges = this.commitChanges.bind(this);
        this.onEditingAppointmentChange = this.onEditingAppointmentChange.bind(this);
        this.onAddedAppointmentChange = this.onAddedAppointmentChange.bind(this);
        this.appointmentForm = connectProps(AppointmentFormContainer, () => {
            const {
                editingFormVisible,
                editingAppointment,
                data,
                addedAppointment,
                isNewAppointment,
                previousAppointment,
            } = this.state;

            const currentAppointment = data
                .filter(appointment => editingAppointment && appointment.id === editingAppointment.id)[0]
                || addedAppointment;
            const cancelAppointment = () => {
                if (isNewAppointment) {
                    this.setState({
                        editingAppointment: previousAppointment,
                        isNewAppointment: false,
                    });
                }
            };

            return {
                visible: editingFormVisible,
                appointmentData: currentAppointment,
                commitChanges: this.commitChanges,
                visibleChange: this.toggleEditingFormVisibility,
                onEditingAppointmentChange: this.onEditingAppointmentChange,
                cancelAppointment,
            };
        });
    }

    componentDidUpdate() {
        this.appointmentForm.update();
    }

    onEditingAppointmentChange(editingAppointment) {
        console.log('Ediiting appointmern');
        console.log(editingAppointment)
        this.setState({ editingAppointment });
    }

    onAddedAppointmentChange(addedAppointment) {
        console.log('onAddedAppointmentChange');
        console.log(addedAppointment);
        this.setState({ addedAppointment });
        const { editingAppointment } = this.state;
        if (editingAppointment !== undefined) {
            this.setState({
                previousAppointment: editingAppointment,
            });
        }
        this.setState({ editingAppointment: undefined, isNewAppointment: true });
    }

    setDeletedAppointmentId(id) {
        this.setState({ deletedAppointmentId: id });
    }

    toggleEditingFormVisibility() {
        const { editingFormVisible } = this.state;
        this.setState({
            editingFormVisible: !editingFormVisible,
        });
    }

    toggleConfirmationVisible() {
        const { confirmationVisible } = this.state;
        this.setState({ confirmationVisible: !confirmationVisible });
    }

    commitDeletedAppointment() {
        this.setState((state) => {
            const { data, deletedAppointmentId } = state;
            const nextData = data.filter(appointment => appointment.id !== deletedAppointmentId);

            return { data: nextData, deletedAppointmentId: null };
        });
        this.toggleConfirmationVisible();
    }

    commitChanges({ added, changed, deleted }) {


        this.setState((state) => {
            let { data } = state;
            let endDate;



            if (added) {
                endDate = new Date(added.startDate);
                let duration = added.duration ? added.duration : 1;
                endDate.setHours(endDate.getHours() + duration);
                const startingAddedId = data.length > 0 ? data[data.length - 1].id + 1 : 0;
                data = [...data, { id: startingAddedId, ...added, request: 1, title: 'Request', endDate, duration }];
            }
            if (changed) {

                data = data.map(appointment => {
                    if (changed[appointment.id]) {

                        console.log('here');
                        endDate = new Date(changed[appointment.id].startDate);
                        endDate.setHours(endDate.getHours() + changed[appointment.id].duration);
                        console.log('appo');
                        console.log(appointment);
                        console.log(changed[appointment.id]);
                        return ({ ...appointment, ...changed[appointment.id], endDate })
                    }
                    else return appointment

                });


            }
            if (deleted !== undefined) {
                this.setDeletedAppointmentId(deleted);
                this.toggleConfirmationVisible();
            }
            console.log(data);
            return { data, addedAppointment: {} };
        });
    }

    render() {
        const {
            currentDate,
            data,
            confirmationVisible,
            editingFormVisible,
            startDayHour,
            endDayHour,
        } = this.state;
        this.currentDateChange = (currentDate) => { this.setState({ currentDate }); };

        const { classes } = this.props;
        console.log(data);
        return (
            <Paper style={{ height: '600px', transform: 'scale(0.9)', top: '-90px' }} >
                <Scheduler
                    data={data}

                >
                    <ViewState
                        currentDate={currentDate}
                        onCurrentDateChange={this.currentDateChange}
                    />
                    <EditingState
                        onCommitChanges={this.commitChanges}
                        onEditingAppointmentChange={this.onEditingAppointmentChange}
                        onAddedAppointmentChange={this.onAddedAppointmentChange}
                    />
                    <WeekView
                        startDayHour={startDayHour}
                        endDayHour={endDayHour}
                    />
                    <MonthView />
                    <EditRecurrenceMenu />
                    <Appointments />
                    <Resources
                        data={resources}
                    />
                    <AppointmentTooltip
                        showOpenButton={true ? true : false}
                        showCloseButton
                        showDeleteButton={true ? true : false}
                    />
                    <Toolbar />
                    <DateNavigator />

                    <ViewSwitcher />
                    <AppointmentForm
                        overlayComponent={this.appointmentForm}
                        visible={editingFormVisible}
                        onVisibilityChange={this.toggleEditingFormVisibility}
                    />
                </Scheduler>

                <Dialog
                    open={confirmationVisible}
                    onClose={this.cancelDelete}
                >
                    <DialogTitle>
                        Izbrisati termin
            </DialogTitle>
                    <DialogContent>
                        <DialogContentText>
                            Jeste li sigurni da želite izbrisati ovaj termin?
              </DialogContentText>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={this.toggleConfirmationVisible} color="primary" variant="outlined">
                            Odustani
              </Button>
                        <Button onClick={this.commitDeletedAppointment} color="secondary" variant="outlined">
                            Obriši
              </Button>
                    </DialogActions>
                </Dialog>

                <Fab
                    color="secondary"
                    className={classes.addButton}
                    onClick={() => {
                        this.setState({ editingFormVisible: true });
                        this.onEditingAppointmentChange(undefined);
                        this.onAddedAppointmentChange({
                            startDate: new Date(currentDate).setHours(startDayHour),
                            endDate: new Date(currentDate).setHours(startDayHour + 1),
                        });
                    }}
                >
                    <AddIcon />
                </Fab>
            </Paper>
        );
    }
}

export default withStyles(styles, { name: 'EditingDemo' })(Kalendar);

