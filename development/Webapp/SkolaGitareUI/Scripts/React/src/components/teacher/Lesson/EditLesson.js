import React from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogTitle from "@material-ui/core/DialogTitle";
import EditIcon from "@material-ui/icons/Edit";
import IconButton from "@material-ui/core/IconButton";
import LessonForm from "./LessonForm";
import Grid from "@material-ui/core/Grid";
import { makeStyles } from "@material-ui/core/styles";
import pink from "@material-ui/core/colors/pink";
import request from "../../../request.js";

const useStyles = makeStyles((theme) => ({
    paper: {
        padding: theme.spacing(2),
        textAlign: "center",
    },
    root: {
        width: "50%",
        margin: "2%",
        padding: theme.spacing(1),
    },
    button: {
        backgroundColor: pink[500],
        marginTop: "5px",
        color: "white",
    },
    contentGrid: {},
}));
export default function EditLesson(props) {
    const classes = useStyles();
    const [open, setOpen] = React.useState(false);
    const [data, setData] = React.useState({
        name: props.lesson?.name ? props.lesson.name : "",
        notes: props.lesson?.notes ? props.lesson.notes : "",
        file: props.lesson?.downloadLink ? props.lesson.downloadLink : {},
    });
    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };
    const handleChange = (field, values) => {
        setData({ ...data, [field]: values });
    };
    const handleSave = () => {


        let body = {
            name: data.name,
            notes: data.notes,
        }
        request(props.accessToken, {
            method: "put",
            url: `Lessons/${props.lesson.id}`,
            data: body
        }).then((data) => {
            props.handleEdit();
            handleClose();
        });

    };
    return (
        <div>
            <IconButton
                color="secondary"
                onClick={handleClickOpen}
                aria-label="add an alarm"
                size="small"
            >
                <EditIcon />
            </IconButton>

            <Dialog
                open={open}
                onClose={handleClose}
                aria-labelledby="form-dialog-title"
            >
                <DialogTitle id="form-dialog-title">{props.title}</DialogTitle>
                <DialogContent>
                    <DialogContentText></DialogContentText>

                    <Grid container spacing={2} justify="center" alignItems="center">
                        <Grid item xs={6} className={classes.contentGrid} align="center">
                            <LessonForm
                                lesson={data}
                                handleChange={handleChange}
                                isNew={false}
                                title={"Uredi Lekciju"}
                            ></LessonForm>
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Cancel
          </Button>
                    <Button color="primary" onClick={handleSave}>
                        Save
          </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}
