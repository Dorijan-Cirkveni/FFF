import * as React from 'react';
import Paper from '@material-ui/core/Paper';
import {
    Scheduler,
    WeekView,
    Appointments,
} from '@devexpress/dx-react-scheduler-material-ui';


export default () => (
    <Paper>
        <Scheduler data={[]} height={660}>
            <WeekView startDayHour={7} endDayHour={21} />
            <MonthView />

            <Appointments />
        </Scheduler>
    </Paper>
);
