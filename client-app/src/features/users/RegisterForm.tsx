import React from "react";
import {ErrorMessage, Form, Formik} from "formik";
import * as Yup from 'yup';
import {Button, Header, Label} from "semantic-ui-react";
import MyTextInput from "../../app/common/form/MyTextInput";
import {observer} from "mobx-react-lite";
import {useStore} from "../../app/stores/store"
import ValidationErrors from "../errors/ValidationErrors";

export default observer(function RegisterForm() {
    const {userStore} = useStore();

    const validationSchema = Yup.object({
        email: Yup.string().required().email(),
        username: Yup.string().required(),
        displayName: Yup.string().required(),
        password: Yup.string().required().matches(new RegExp('(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$'),
            'Password must have 1 Uppercase, 1 lowercase, 1 number, and at least 4 characters'),
        passwordConfirmation: Yup.string().required().oneOf([Yup.ref('password'), null], 'Passwords do not match')
    })

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={{email: '', username: '', displayName: '', password: '', passwordConfirmation: '', error: null}}
            onSubmit={(values, {setErrors}) => userStore.register(values).catch(error =>
                setErrors({error}))}
        >
            {({handleSubmit, isValid, isSubmitting, dirty, errors}) => (
                <Form className={'ui form error'} onSubmit={handleSubmit} autoComplete={'off'}>
                    <Header as={'h2'} content={'Sign up to Reactivities'} color={'teal'} textAlign={'center'}/>
                    <MyTextInput placeholder={'Display name'} name={'displayName'} />
                    <MyTextInput placeholder={'Username'} name={'username'} />
                    <MyTextInput name={'email'} placeholder={'Email'} />
                    <MyTextInput name={'password'} placeholder={'Password'} type={'password'} />
                    <MyTextInput name={'passwordConfirmation'} placeholder={'Confirm Password'} type={'password'} />
                    <ErrorMessage
                        name={'error'} render={() =>
                        <ValidationErrors errors={errors.error} />} />
                    <Button
                        disabled={isSubmitting || !isValid || !dirty}
                        loading={isSubmitting}
                        positive type={'submit'} content={'Register'} fluid
                    />
                </Form>
            )}
        </Formik>
    )
})